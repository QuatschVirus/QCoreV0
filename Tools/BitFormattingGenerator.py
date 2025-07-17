"""
QCoreV0 Tools - Bit Formatting Generator
This script generates a formatted output for bit fields based on user input
"""

from re import compile
from typing import Protocol

FIELD_REGEX = compile(r"^([a-zA-Z_0-9]+) (?:(\d+):(\d+)|(\d+))$")

width_suffixed: list[str] = ["func", "arg"]
count_suffixed: list[str] = ["rd", "rs"]


__version__ = "1.0.0"


class TotalCountGetter(Protocol):
	def __call__(self, name: str) -> int:
		"""Returns the total count of a field."""
		pass


class Field:
	def __init__(self, name: str, index: int, width: int, fieldstart: int = None, fieldstop: int = None, count: int = None, total_count_getter: TotalCountGetter = None):
		self.name = name
		self.fieldstart = fieldstart
		self.fieldstop = fieldstop
		self.index = index
		self.width = width
		self.count = count
		self.total_count_getter = total_count_getter

	@property
	def display_name(self):
		if self.fieldstart is not None and self.fieldstop is not None:
			return f"{self.name} [{self.fieldstop}:{self.fieldstart}]"
		elif self.name in width_suffixed:
			return f"{self.name}{self.width}"
		elif self.name in count_suffixed and self.count is not None and self.total_count_getter(self.name) > 1:
			return f"{self.name}{self.count}"
		else:
			return f"{self.name}"

	@property
	def end(self):
		return self.index + self.width - 1


def main():
	print("Enter fields and bit mappings in order, or '#generate' to generate the formatting. ('fieldName [fieldstart]:[fieldstop]' or 'fieldName [width]')")
	print("fieldstart and fieldstop are inclusive and only used for setting where the entered bits will be placed in the field value, and for calculating the width")
	print("Use '#delete' to remove the last field")

	current_index: int = 0

	fields: list[Field] = []
	counts: dict[str, int] = {}

	def total_count_getter(n: str) -> int:
		return counts.get(n, 1)

	def process_input(cmd: str):
		nonlocal current_index
		cmd = cmd.strip()
		if cmd == "#generate":
			return False
		elif cmd == "#delete":
			if not fields:
				print("No fields to delete")
				return True
			fields.pop()
			print("Last field deleted")
			return True
		elif cmd == "":
			return True

		match = FIELD_REGEX.match(cmd)
		if not match:
			print("Invalid input format. Please use 'fieldName [fieldstart]:[fieldstop]' or 'fieldName [width]'")
			return True

		name = match.group(1)
		if name in counts:
			counts[name] += 1
		else:
			counts[name] = 1

		if match.group(2) is not None and match.group(3) is not None:
			begin = int(match.group(2))
			end = int(match.group(3))
			if begin > end:
				print("Invalid field range. Start must be less than or equal to stop")
				return True
			w = end - begin + 1
			fields.append(Field(name, current_index, w, begin, end, counts[name], total_count_getter))
			current_index += w
		elif match.group(4) is not None:
			w = int(match.group(4))
			fields.append(Field(name, current_index, w, count=counts[name], total_count_getter=total_count_getter))
			current_index += w
		else:
			print("Invalid input format. Please use 'fieldName [fieldstart]:[fieldstop]' or 'fieldName [width]'.")
			return True
		return True

	looping = True

	print("")
	while looping:
		inp = input("Field: ")
		for c in inp.split(","):
			if not process_input(c.strip()):
				looping = False
				break

	fields.reverse()

	upper = "| "
	lower = "| "

	digits = len(str(current_index - 1))

	for field in fields:
		name_len = len(field.display_name)
		min_spacing = 2  # default minimum spacing
		width = (field.width - 2) * 2 + digits * 2 + (field.width - 1)

		if name_len > width:
			min_spacing = -((name_len - (field.width - 1)) // -2)  # Ceiling division

		width = (field.width - 2) * min_spacing + max(digits, min_spacing) * 2 + (field.width - 1)

		stop = str(field.end).rjust(digits, "0")
		start = str(field.index).rjust(digits, "0")

		upper += stop.ljust(min_spacing) + ("'" + " " * min_spacing) * (field.width - 2) + "'" + start.rjust(min_spacing, ) + " | "
		lower += field.display_name.center(width) + " | "

	upper = upper.strip()
	lower = lower.strip()

	print("\nResult:")
	print(upper)
	print(lower)

	try:
		import pyperclip
		pyperclip.copy(upper + "\n" + lower)
		print("\nFormatted output copied to clipboard")
	except ImportError:
		print("\npyperclip not installed. Output not copied to clipboard")


if __name__ == "__main__":
	print("\n--- QCoreV0 Tools - Bit Formatting Generator ---\n")
	main()
	print("\n--- END - QCoreV0 Tools - Bit Formatting Generator - END ---\n")
