    title   System Calls
	type	8521
		
    program

SystemCall     equ     20f1h

	; System Calls
    global          FBDrawGraphic
    global          FBFillColor
	global			FBDrawLineV
	global			FBDrawLineH
    global          FBFillColorRect
	global			IOInputScan

	; Extended System Calls
	global			FBDrawGraphicF
	
	; Unknown System Calls
	global			FBDrawGraphic2
	
; ===========================================================================
;    0x2E - FBFillColorRect
;    FUNCTION: Draws a block of a filled color on-screen
;    INPUT:
;        r10 = Horizontal block size
;        r11 = Vertical block size
;        r12 = Color to fill block with
;    OUTPUT: N/A
; ===========================================================================
FBFillColorRect:
	pushw   rr6
	mov     r7,#2eh
	jmp		SystemCall
; ===========================================================================
;    0x1B - FBFillColor
;    FUNCTION: Fills the framebuffer with a specified color
;    INPUT:
;        r8 = Color to fill with
;    OUTPUT: N/A
; ===========================================================================
FBFillColor:
	pushw	rr6
	mov		r7,#1bh
	jmp		SystemCall
	
; ===========================================================================
;    0x1D - FBDrawGraphic
;    FUNCTION: Draws specified graphics to the framebuffer
;    INPUT:
;        r8 = Horizontal framebuffer displacement
;        r9 = Vertical framebuffer displacement
;        r10 = Horizontal graphic offset displacement
;        r11 = Vertical graphic offset displacement
;        r12 = Horizontal graphic size
;        r13 = Verical graphic size
;        r14 = Bank #
;        r15 = Drawing behavior
;            - 0 = Completely overwrite
;            - 1 = Pixel color 0 is transparent (lightest grey)
;            - 8 = XOR mode (???)
;    OUTPUT: N/A
; ===========================================================================
FBDrawGraphic:
	pushw   rr6
	mov     r7,#1dh
	jmp		SystemCall
	
; ===========================================================================
;   0x26 - FBDrawLineV
; 	FUNCTION: Draws a completely vertical line on-screen (draws from top to bottom)
; 	INPUT:
; 		r8 = Starting horizontal coordinate
; 		r9 = Starting vertical coordinate
; 		r10 = Line length
; 		r11 = Color to draw line with
; 		r12 = Line width
; 		r13 = ??? (Look into what exactly this is)
; 			- 0 = Line gets drawn
; 			- 1 = Line does not get drawn
;    OUTPUT: N/A
; ===========================================================================
FBDrawLineV:
	pushw	rr6
	mov		r7,#26h
	jmp		SystemCall
	
; ===========================================================================
;   0x26 - FBDrawLineH
; 	FUNCTION: Draws a completely horizontal line on-screen (draws from left to right)
; 	INPUT:
; 		r8 = Starting horizontal coordinate
; 		r9 = Starting vertical coordinate
; 		r10 = Line length
; 		r11 = Color to draw line with
; 		r12 = Line width
; 		r13 = ??? (Look into what exactly this is)
; 			- 0 = Line gets drawn
; 			- 1 = Line does not get drawn
;    OUTPUT: N/A
; ===========================================================================
FBDrawLineH:
	pushw	rr6
	mov		r7,#27h
	jmp		SystemCall
	
; ===========================================================================
;	0x2F - IOInputScan
;	FUNCTION: Gets the player's input
;	INPUT: 	  N/A
;	OUTPUT:
;	    r0 = Player's inputted keys (document how this works better)
;	        - 0x83 = Up
;	        - 0x84 = Down
;	        - 0x85 = Left
;	        - 0x86 = Right
;	        - 0x8B = A
;	        - 0x8C = B
;	        - 0x8D = C
;	        - 0x8E = D
; ===========================================================================
IOInputScan:
	pushw	rr6
	mov		r7,#2fh
	jmp		SystemCall
; ===========================================================================
; ===========================================================================
FBDrawGraphic2:
	pushw	rr6
	mov		r7,#56h
	jmp		SystemCall
; ===========================================================================
;    0x1D - FBDrawGraphicf
;    FUNCTION: Draws specified graphics to the framebuffer using the fadeStep
;    INPUT:
;        r8 = Horizontal framebuffer displacement
;        r9 = Vertical framebuffer displacement
;        r10 = Horizontal graphic offset displacement
;        r11 = Vertical graphic offset displacement
;        r12 = Horizontal graphic size
;        r13 = Verical graphic size
;        r14 = Bank #
;        r15 = Drawing behavior
;            - 0 = Completely overwrite
;            - 1 = Pixel color 0 is transparent (lightest grey)
;            - 8 = XOR mode (???)
;    OUTPUT: N/A
; ===========================================================================
FBDrawGraphicF:
	push	r0
	mov		r0,fadeStep		; copy the current fade step to r0
	cmp		r0,#127
	br		ule,FBFadeGraphicC
	cmp		r0,#159
	br		ule,FBFadeGraphic2
	cmp		r0,#191
	br		ule,FBFadeGraphic3
	pop		r0
	; fill with black
	mov		r8,#3
	jmp		FBFillColor	

FBFadeGraphic3:
	inc		r14
FBFadeGraphic2:	
	inc		r14
FBFadeGraphicC:
	pop		r0
    pushw   rr6
    mov     r7,#1dh
    jmp		SystemCall
; ===========================================================================
	end