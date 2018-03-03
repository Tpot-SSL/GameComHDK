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
		push	r0					; store r0 onto the stack
		push	r1					; store r1 onto the stack

		mov	r0, 0112h			    ; load the DMG_timer into rr2
		mov	r1, prevVtick			; load the previous V-Int tick into r1
		
WaitForVInt_Wait:
		cmp	r0,	r1					; is the timer on the same tick as before?
		br	eq,	WaitForVInt_Wait	; if so, loop and wait
		
		mov	prevVtick,r0			; store the new V-Int tick

		pop	r1						; restore r1 from the stack		
		pop	r0						; restore r0 from the stack
		ret							; return
; ===========================================================================	
		end
; ===========================================================================	