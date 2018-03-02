asm8521 -s16 -W -E -L source.asm
link8521 source
hex8521 -P -a1 -F source
hex2bin source.hex gcbuild.bin