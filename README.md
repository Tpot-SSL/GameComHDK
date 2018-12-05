# Game.com HDK
A ready homebrew development kit for the Tiger Game.com, as well as various tools and libraries for handling game.com data and rom files.
This project is still a work in progress, but it's already able to compile game.com code, and handle game.com graphics.

Do keep in mind that writing game.com code is still in it's early stages.
Graphics, button input, and logic are all documented and working, and the HDK includes various examples and templates to make that all easier.
But I/O ports, additional rom carts, and audio are still somewhat a mystery.

Resources:
  Game.Com Microcontroller Docs: http://pdf.datasheetcatalog.com/datasheet/Sharp/mXuyzuq.pdf
  
  Hexcode Documentation: [located in the root of the repo]
  
  Explorer Shell Extensions: https://github.com/GerbilSoft/rom-properties

Todo:
  - ROM code Disassembler
  - Simple custom assembler
  - Graphics and VRAM emulation
  - More System Call research and documentation
  - Documentation on templates and helper functions
  - Upload existing homebrew demos to a repository
  - More template projects, tutorials and guides.
  - Audio handling and tools.
  - Map and placement building tools??
  
If you'd like to see the development kit in action you can check out our ongoing homebrew project: https://github.com/Tpot-SSL/Tetris_GameCOM

Also big shoutout and thank you to TheStoneBanana for helping a lot on understanding the game.com assembler and assembly language!
He's also made some homebrew demos you can check out for more examples of use:
https://github.com/TheStoneBanana/GameCOM-FMV
