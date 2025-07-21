Each core has 32 general-purpose 32bit registers, accessed by a 5 bit address. They are named `x0` through `x31` .
`x0` is a bit special, in that it doesn't really store any data. Reading from it will always produce `0` , writing to it will discard the data written. It is also named `zero` or `null` (For Linux users: it is basically `/dev/null`)
The registers by themselves do not determine whether they use signed or unsigned values. It is the programmers responsibility to take care of that

There are also a multitude of Control and Status (CS) registers, which are used by the core to control how it works or inform the code about status. These also have defined fields - sometimes called flags if they are one bit wide - and are accessed with special instructions. They can cause side effects when writing to or reading from them.

## CS Registers
### AS - Arithmetic Status
This register reports any states related to arithmetic operations. It is completely read-only to the code.

#### CB - Carry / Borrow
Set to 1 when a arithmetic operation needs to use a carry or borrow bit to accurately display its value. This can happen in three cases:

#### OV - Overflow
Set to 1 when an arithmetic operation exceeds the bounds of the signed number.
This can, for example, happen when adding `0x7FFF_FFFF` (2_147_483_647) and `0x7FFF_FFFF` (2_147_483_647), which would logically result in `0xFFFF_FFFE`, which is -2 when interpreted as a signed number. So, ion that case, OV would be set to indicate that the actual result of the operation exceeds the bounds of a signed 32-bit number

### SG - Sign
Indicates the sign of the result of an arithmetic operation. 1 means Negative, 0 means positive. Keep in mind the value is not changed when the result is 0. This is calculated separately from the stored result and is thus always correct, even if the result overflowed.
To take the example from the `OV` flag, adding `0x7FFF_FFFF` (2_147_483_647) and `0x7FFF_FFFF` (2_147_483_647 would logically result in `0xFFFF_FFFE`, which is -2 when interpreted as a signed number. However, the `SG` flag will still be set to 0 for a positive number, because the "actual" result of the calculation (4_294_967_294) is positive

#### ZR - Zero
Indicates whether the result of an arithmetic operation is 0. This is calculated separately from the stored result, and will thus ignore any overflow. 

### RM - Register Mode
This register is used to tell the core and programs whether to interpret the corresponding register as signed or unsigned. Each bit controls the mode of the register numbered with its index (e.g. `RM[2]` sets the mode of `x2`, `RM[15]` of `x15`, etc.)
Writing to this register will not cause side effects, meaning the values stored in the register will not be converted to an identical numerical value. 0 means signed, 1 means unsigned mode. Bit 0 controls whether instructions default to signed or unsigned mode.


Any instructions that deal with multiple registers will assume the default mode unless **all** involved registers are set to the other mode. When a register in unsigned mode is used in an instruction in signed mode, all values that cannot be represented as a signed 32-bit value are treated as the biggest value that can be represented (`0x7FFF_FFFF` or 2_147_483_647). Conversely, when a register in signed mode is used with an instruction in unsigned mode, all values lower than 0 are treated as 0.

### STK - Call Stack
This register contains the top value of the call stack. Writing to it will push a new value, reading from it will pop the top value, meaning the value next in line is loaded immediately afterwards. It is advised to not directly push values to the call stack, but you are left open to do so at your own discretion.

### ARQ - Argument Queue
This register contains the top element of the Argument Queue, used to store second order arguments. Writing to it will enqueue a new value, reading from it will dequeue the top value.


