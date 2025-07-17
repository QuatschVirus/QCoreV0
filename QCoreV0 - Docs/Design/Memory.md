The memory includes all of the RAM and the ROM, as well as the call stack.

## RAM
RAM is divided into pages. Each page 16 bits of address space, resulting in about 65535 Bytes per page. Each [[Core]] has its own zero page. It is always loaded and ready for access, and not shared with any other Core under any circumstance. It is accessible and writable in all privilege levels. There is also a private privilege-level-specific page for each core, which is also not shared. It does not allow access by any other core or privilege level. There are four more pages for each privilege level that are shared between all cores, also not accessible by other privilege levels. This gives you 16 pages of pre-reserved memory that is always loaded and ready for access. All other pages need to be loaded before being able to be accessed, which takes longer. When loading a page, the one previous and two following it are also pre-loaded to speed up long reads. You can disable this behavior using a CSR.

The lower 16 bits of the 32 bit address index into the page, the upper 16 bits are responsible for selecting the page. Trying to read from or write to an unloaded page will load it and cache its neighbors as described above. You can also preload pages. Un-accessed pages will be unloaded 
after a set number of memory operations, specifiable via a CSR.




## ROM
The ROM is a fully separate persistent memory that stores the base program. It is accessed by a separate LOAD instruction (it's read-only, so no writing), and has a full 32 bit address space. It also divided into 16bit addressable pages, but more for layout and optimization reasons. The pages surrounding the current page (1 behind, 2 ahead) are always dynamically loaded, as well as up to 16 pages marked with an importance flag (note to self: Implement this!).