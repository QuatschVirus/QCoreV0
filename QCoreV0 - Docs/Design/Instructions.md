## Fields
Each instruction has multiple fields, which are named sections of the instruction that fulfill some function. Some common fields include
- `size` : The instruction's size code. See [[#Sizing]]
- `opcode`: The instruction's opcode. See [[#Opcodes]]
- `func`: The instruction's specific function within the opcode. Usually suffixed by its width (A 3 bit function field would be `func3`, a 7 bit `func7`, etc.)
- `rd`: The destination register. When multiple are used, it is suffixed by an index
- `rs`: The source register. When multiple are used, it is suffixed by an index
- `imm`: An immediate value. These fields usually only encode parts of the full immediate value, and are marked as such. For example, an `imm[15:0]` field would encode the 16 least significant bits of the immediate value. 
- `arg`: "Arguments" for functions telling them how to operate. Usually suffixed by its width (A 3 bit argument field would be `arg3`, a 7 bit `arg7`, etc.)

## Sizing
Instructions can have different sizes, based on the two least significant bits of the instruction

| op[1:0] | Instruction length | Description                      |
| ------- | ------------------ | -------------------------------- |
| `00`    | 32 bits            | The default                      |
| `01`    | 64 bits            | Used for Extended Instructions   |
| `10`    | 16 bits            | Used for Compressed Instructions |
| `11`    | 16 bits            | Used for Compressed Instructions |
Do note that everything in the simulator should work with only the default 32 bit instructions, with the longer instructions providing some ease of use and the shorter instructions being used to have the code take up less space

## Opcodes

## Default Instructions
These are the default 32 bit instructions. They should be all you need to fully operate the core.

### Instructions
#### Integer Register-Register Arithmetic

##### ADD
The value's of `rs1` and `rs2` are added together and the result is written to `rd`.
When in signed mode, the Overflow Flag (`OV`) is set when the result of an addition would be greater or smaller than a signed 32-bit integer can provide, indicating the result has possibly switched signs.
When in unsigned mode, the Carry/Borrow Flag (`CB`) is set when the result of an addition would be greater than an unsigned 32-bit integer can provide.

##### SUB
The value of `rs2` is subtracted from `rs1` and the result is written to `rd`.
When the result is greater or smaller than a signed or unsigned 32-bit integer can provide, the Carry/Borrow Flag (`CB`) is set.

Used by the pseudoinstruction `NEG rs rd` as `SUB x0 rs rd`

##### AND
Performs a logical AND operation on `rs1` and `rs2` and writes the result to `rd`.  The result can be inverted using \[an argument], which is aliased to the `NAND` pseudoinstruction.

##### OR
Performs a logical OR operation on `rs1` and `rs2` and writes the result to `rd`.  The result can be inverted using \[an argument], which is aliased to the `NOR` pseudoinstruction.

Used by the pseudoinstruction `INV rs rd` as `NOR rs x0 rd` 

##### XOR
Performs a logical XOR (exclusive OR) operation on `rs1` and `rs2` and writes the result to `rd`.  The result can be inverted using \[an argument], which is aliased to the `XNOR` pseudoinstruction.

##### SFT
Shifts the bits in `rs1` by the amount found in the least significant 5 bits of `rs2` and writes the result to `rd`.
The direction and kind of operation are determined by \[an argument]

##### CMP
Compares the values of `rs1` and `rs2`, and sets `rd` according to some rules. You can select a different ruleset using \[an argument], for example to use it for a `JMPZ` or `BRNZ`

This is how the value written to `rd` is determined:

| `rule`          | `000` ($=$) | `001` ($<$) | `010` ($>$) | `011` ($\neq$) | `100` ($\leq$) | `101` ($\geq$) | `110` (= -) | `111` (reserved) |
| --------------- | ----------- | ----------- | ----------- | -------------- | -------------- | -------------- | ----------- | ---------------- |
| `rs1` < `rs2`   | -1          | 0           | -1          | 0              | 0              | -1             | -1          | 0                |
| `rs1` = `rs2`   | 0           | 2           | 2           | 2              | 0              | 0              | 2           | 0                |
| `rs1` > `rs2`   | 1           | 1           | 0           | 0              | 1              | 0              | 1           | 0                |
| `rs1` = -`rs2`* | 3           | 3           | 3           | 0              | 3              | 3              | 0           | 0                |
\* This case has precedence over the other cases, and will not happen when in unsigned mode

Used by the pseudoinstruction `SGN rs, rd` as `CMP rs, x0, rd, 0x0`

*space for one more for func3*

#### Integer Register-Immediate Arithmetic
##### ADDI
Add the value of the immediate to `rs` and write the result to `rd`
When in signed mode, the Overflow Flag (`OV`) is set when the result of an addition would be greater or smaller than a signed 32-bit integer can provide, indicating the result has possibly switched signs.
When in unsigned mode, the Carry/Borrow Flag (`CB`) is set when the result of an addition would be greater than an unsigned 32-bit integer can provide.

##### SUBI
The value of the immediate is subtracted from `rs` and the result is written to `rd`.
When the result is greater or smaller than a signed or unsigned 32-bit integer can provide, the Carry/Borrow Flag (`CB`) is set.

##### ANDI
Performs a logical AND operation on `rs` and the immediate value and writes the result to `rd`.

##### ORI
Performs a logical OR operation on `rs` and the immediate value and writes the result to `rd`.

##### XORI
Performs a logical XOR (exclusive OR) operation on `rs` and the immediate value and writes the result to `rd`.

##### SFTI
Shifts the bits in `rs` by the amount found in the least significant 5 bits of the immediate and writes the result to `rd`.
The direction and kind of operation are determined by the three most significant bits of the immediate value. 

##### SZLE
Sets `rd` to 0 when `rs` is less than or equal to the immediate value, otherwise sets `rd` to 1

#### Memory Loading
##### LD
Loads a full word from RAM at the address specified by adding `rs` and the immediate value and writing it to `rd`

##### LDR
Loads a full word from ROM at the address specified by adding `rs` and the immediate value and writing it to `rd`

##### LDH
Loads a half word from RAM at the address specified by adding `rs` and the immediate value, sign-extending and writing it to the least significant bits of `rd`

##### LDHR
Loads a half word from ROM at the address specified by adding `rs` and the immediate value, sign-extending and writing it to the least significant bits of `rd`

##### LDB
Loads a byte from RAM at the address specified by adding `rs` and the immediate value and writing it to the least significant bits of `rd`.

##### LDBR
Loads a byte from ROM at the address specified by adding `rs` and the immediate value and writing it to the least significant bits of `rd`.

##### LDU
Loads a half word from RAM at the address specified by adding `rs` and the immediate value and writing it to the least significant bits of `rd`

##### LDUR
Loads a half word from ROM at the address specified by adding `rs` and the immediate value and writing it to the least significant bits of `rd`

#### Memory Storing
##### SD
Stores a full word from `rs2` in RAM at the address specified by adding `rs1` and the immediate value

##### SDR
Stores a full word from `rs2` in ROM at the address specified by adding `rs1` and the immediate value

##### SDH
Stores a half word from the least significant bits of `rs2` in RAM at the address specified by adding `rs1` and the immediate value

##### SDHR
Stores a half word from the least significant bits of `rs2` in ROM at the address specified by adding `rs1` and the immediate value

##### SDB
Stores a byte from the least significant bits of `rs2` in RAM at the address specified by adding `rs1` and the immediate value

##### SDBR
Stores a byte from the least significant bits of `rs2` in ROM at the address specified by adding `rs1` and the immediate value

##### SDU
Stores a half word from the least significant bits of `rs2` in RAM at the address specified by adding `rs1` and the immediate value

##### SDUR
Stores a half word from the least significant bits of `rs2` in ROM at the address specified by adding `rs1` and the immediate value

#### Control Flow - Unconditional Jumps 

##### JMP
Pushes the address of the next instruction to the call stack, then sets the program counter to the sum of `rs` and the immediate value shifted left by one to automatically align the address to 16 bit and double the immediate range of the instruction

##### JMPR
Pushes the address of the next instruction to the call stack, then **adds** the sum of `rs` and the immediate value shifted left by one to the program counter. This is useful for performing relative jumps. For example, `JMPR x0 4` would jump `(4 << 1) * 8 = 8 * 8 = 64` bits ahead. Placing a colon (`:`) in front of the immediate value will instruct the assembler to accumulate the size of that number of following instructions, useful for jumping ahead a specific number of instructions regardless of their sizes. If `rs` is not specified, `x0` is assumed

#### Control Flow - Register-based Conditional Branch
##### BRN
Pushes the address of the next instruction to the call stack, then sets the program counter to the sum of `rs` and the immediate value shifted left by one to automatically align the address to 16 bit and double the immediate range of the instruction if the Zero Flag (`ZR`) is set

##### BRNR
Pushes the address of the next instruction to the call stack, then adds the sum of `rs` and the immediate value shifted left by one to the program counter if the Zero Flag (`ZR`) is set

#### Control Flow - Immediate-based Conditional Branch
##### BRNI
Pushes the address of the next instruction to the call stack, then sets the program counter to the immediate value shifted left by one to automatically align the address to 16 bit and double the immediate range of the instruction if the Zero Flag (`ZR`) is set

##### BNRI
Pushes the address of the next instruction to the call stack, then adds the immediate value shifted left by one to the program counter if the Zero Flag (`ZR`) is set

####  Control Flow - Returning
##### RTN
Pops the top value from the call stack and writes it to the program counter. Does nothing if the call stack is empty.

##### RTNZ
Pops the top value from the call stack and writes it to the program counter if the Zero Flag (`ZR`) is set. Does nothing if the call stack is empty.

##### RTNR
Pops the top value from the call stack, adds the value of the immediate shifted left by one to automatically align the address to 16 bit and double the immediate range of the instruction and writes it to the program counter. Does nothing if the call stack is empty.

##### RTZR
Pops the top value from the call stack, adds the value of the immediate shifted left by one and writes it to the program counter if the Zero Flag (`ZR`) is set. Does nothing if the call stack is empty.