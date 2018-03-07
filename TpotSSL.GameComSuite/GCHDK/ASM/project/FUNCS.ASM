; ===========================================================================	
		title   Various Functions
		type	8521
		program

; ===========================================================================
; Subroutine to read the player's input
; ===========================================================================	
ReadInput:
;		call	SCAN_KEY		; use the system call to read the current inputs
		mov	playerInput,r0		; store the input to RAM
		ret
; ===========================================================================
; Subroutine to wait for a vertical interrupt
; ===========================================================================	
WaitForVInt:
		pushw	rr2					; store rr2 onto the stack
		push	r0					; store r0 onto the stack
		push	r1					; store r1 onto the stack

		movw	rr2,#0112h			; load the DMG_timer into rr2
		mov	r1, prevVtick			; load the previous V-Int tick into r1
		
WaitForVInt_Wait:
		mov	r0,	0(rr2)				; load the value of the timer into r0
		cmp	r0,	r1					; is the timer on the same tick as before?
		br	eq,	WaitForVInt_Wait	; if so, loop and wait
		
		mov	prevVtick,r0			; store the new V-Int tick

		pop	r1						; restore r1 from the stack		
		pop	r0						; restore r0 from the stack
		popw	rr2					; restore rr2 from the stack
		ret							; return
		
StrCharY        equ     224
StrCharBank     equ     39
StrCharWid      equ     6
StrCharHgt      equ     6

; ===========================================================================	
; Subroutine to write a character on screen.
; Input:
; rr0 = String's address
; r8 = String's X-position
; r9 = String's Y-position
; ===========================================================================
FBDrawChar:
		mov		r11,  	 r10
		mov		r10,  	 #0
		mult	rr10, 	 #StrCharWid
		mov		r10,  	 r11
		mov		r11,   	 #StrCharY
		mov		r12,  	 #StrCharWid
		mov		r13,  	 #StrCharHgt
		mov		r14,  	 #StrCharBank
		mov		r15,  	 #01h
		cmp		currPage,#1
		
		call	FBDrawGraphic		
		ret

; ===========================================================================	
; Subroutine to write a character on screen with XOR-mode
; Input:
; rr0 = String's address
; r8 = String's X-position
; r9 = String's Y-position
; ===========================================================================
FBDrawCharX:
		mov		r11,	 r10
		mov		r10,	 #0
		mult	rr10,	 #StrCharWid
		mov		r10,	 r11
		mov		r11,	 #StrCharY
		mov		r12,	 #StrCharWid
		mov		r13,	 #StrCharHgt
		mov		r14,	 #StrCharBank
		mov		r15,	 #01h
		cmp		currPage,#1
		br		eq, 	 FBDrawCharX_1
		add		r15,	 #02h
FBDrawCharX_1:
		call	FBDrawGraphic		
		cmp		r0,		 #36			; is the character a space?
		br		eq,	 	 FBDrawCharX_END ; if so, don't do the extra XORing
		add		r11,	 #12
		mov		r15,	 #09h
		cmp		currPage,#1
		br		eq,		 FBDrawCharX_2
		add		r15,	 #02h
FBDrawCharX_2:
		call	FBDrawGraphic			
FBDrawCharX_END:
		ret
; ===========================================================================	
; Subroutine to write a string on screen
; Input:
; rr0 = String's address
; r8  = String's X-position
; r9  = String's Y-position
; ===========================================================================
FBDrawString:
		
DrwStr_Chr:
		mov		r0,	(rr2)+
		cmp		r0,	#0FFh			; is the character a terminator?
		br		eq,	DrwStr_END		; if so, branch
		cmp		r0,	#020h			; is the character a space?
		br		eq,	DrwStr_Spc		; if so, branch
		cmp		r0,	#041h			; is the character from the alphabet?
		br		uge,DrwStr_Alph	; if so, branch
		cmp		r0,	#030h			; is the character numeric?
		br		uge,DrwStr_Num		; if so, branch
		jmp		DrwStr_END			; otherwise, panic and leave
		
DrwStr_Spc:
		mov		r0,	#36
		br		DrwStr_Do
				
DrwStr_Num:
		cmp		r0,	#039h			; is the character numeric? (checking for 9)
		br		ugt,DrwStr_END		; if not, panic and leave
		sub		r0,	#030h
		br		DrwStr_Do
				
DrwStr_Alph:
		cmp		r0,	#05Ah			; is the character from the alphabet? (checking for Z)
		br		ugt,DrwStr_END		; if not, panic and leave
		
		sub		r0,	#037h
		br		DrwStr_Do

DrwStr_Do:		
		mov		r10,r0
		call	FBDrawChar			; otherwise, draw the character

DrwStr_NxtChr:
		add		r8,	#6				; increase the X-position for the next character
		br		DrwStr_Chr
		
DrwStr_END:		
		ret
; ===========================================================================	
; Subroutine to write a string on screen using XOR-mode for 4-colors + alpha.
; Input:
; rr0 = String's address
; r8  = String's X-position
; r9  = String's Y-position
; ===========================================================================
FBDrawStringX:
DrwStrX_Chr:
		mov		r0,	(rr2)+
		cmp		r0,	#0FFh			; is the character a terminator?
		br		eq,	DrwStrX_END		; if so, branch
		cmp		r0,	#020h			; is the character a space?
		br		eq,	DrwStrX_Spc		; if so, branch
		cmp		r0,	#041h			; is the character from the alphabet?
		br		uge,DrwStrX_Alph	; if so, branch
		cmp		r0,	#030h			; is the character numeric?
		br		uge,DrwStrX_Num		; if so, branch
		jmp		DrwStrX_END			; otherwise, panic and leave
		
DrwStrX_Spc:
		mov		r0,	#36
		br		DrwStrX_Do
				
DrwStrX_Num:
		cmp		r0,	#039h			; is the character numeric? (checking for 9)
		br		ugt,DrwStrX_END		; if not, panic and leave
		sub		r0,	#030h
		br		DrwStrX_Do
				
DrwStrX_Alph:
		cmp		r0,	#05Ah			; is the character from the alphabet? (checking for Z)
		br		ugt,DrwStrX_END		; if not, panic and leave
		
		sub		r0,	#037h
		br		DrwStrX_Do

DrwStrX_Do:		
		mov		r10,r0
		call	FBDrawCharX			; otherwise, draw the character

DrwStrX_NxtChr:
		add		r8,	#6				; increase the X-position for the next character
		br		DrwStrX_Chr
		
DrwStrX_END:		
		ret
; ===========================================================================	
		end
; ===========================================================================	