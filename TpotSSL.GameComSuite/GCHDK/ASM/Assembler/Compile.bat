asm8521 -s16 -W -E -L "source/MAIN.ASM"
link8521 "source/MAIN"
hex8521 -P -a1 -F "source/MAIN"
hex2bin "source/MAIN.hex" build.bin
exit