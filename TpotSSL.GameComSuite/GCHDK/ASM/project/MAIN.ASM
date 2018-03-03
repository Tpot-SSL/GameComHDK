; ===========================================================================
		title   ProjectTitle		    ; Project Name
		type	8521					; CPU Type
		org		4000h
; ---------------------------------------------------------------------------		
		NLIST
		include source/EQUATES.INC		; Equates
		LIST		
; ---------------------------------------------------------------------------		
		program	
; ===========================================================================	
		db      08h					; Magic Number
		db      20h					; Magic Number
		dw      GameEntry			; Entry Label
		db      00000011b           ; Magic Number part 2
		dm      'TigerDMGC'			; Magic Number part 3
		db      24h	         		; Icon Bank #
		db      0                  	; Icon X Coords.
		db      160                 ; Icon Y Coords.
		dm      'Project00'			; Game Title (Must be 9 characters long)
		dw      1A18h				; Game ID
		db      97h
		db      0
		dw      0
; ===========================================================================	
GameEntry:
		clr     r0
		cmp     r2,#0			; is the Game.COM turning on from a cold boot?
		br      eq,GameInit		; if so, branch
		cmp     r2,#1			; has the game been entered from the main menu?
		jmp     eq,GameExe		; if so, branch
		cmp     r2,#2			; is the game being closed?
		br      eq,GameInit		; if so, branch
		cmp     r2,#3			; is the game being reset?
		br      eq,GameInit		; if so, branch
		mov     r0,#-1			; otherwise, indicate that none of the above conditions were able to be met
		ret						; return
; ---------------------------------------------------------------------------
GameInit:
		ret						; return, doing nothing
; ---------------------------------------------------------------------------		
GameReset:
GameExe:
		mov		fadeStep,	#255		; Set fade to solid black
		mov 	gameState,	#0			; Set game state to 0 (first)
GameState:
		call    GameFadeIn				; Fade In
		call	GameUpdate				; Start Game Loop
GameUpdate:
		; Insert Update Code Here
		jmp		GameUpdate				; Jump back to the beginning of the routine (loop)
GameDraw:
		; Insert Draw Code Here
		ret								; Return
GameFadeIn:
		call	WaitForVInt				; Wait for Vblank
		mov		r0,	fadeStep			; Copy fadestep into register 0
		dec		r0						; Decrease register 0 by 1
		mov		fadeStep,r0				; Copy register 0 back into fadetep

		call	GameDraw				; Draw Game
		
		cmp		r0,	#111				; Compare register 0 to the integer 111
		jmp		ne,	GameFadeIn			; If it's not equal (ne) to 111, jump back to the beginning of the routine
		ret								; Return
GameChangeState:
		call    GameFadeOut				; Fade out
		mov		gameState, nextGState	; Change game state
		call    GameFadeIn				; Fade back in
		jmp		GameUpdate				; Return to update
GameFadeOut:
		call	WaitForVInt				; Wait for Vblank
		mov		r0, fadeStep			; Copy fadestep into register 0
		inc		r0						; Increase register 0 by 1
		mov		fadeStep, r0			; Copy register 0 back into fadetep
		
		call	GameDraw				; Draw Game
		
		cmp		r0,	#255				; Compare register 0 to the integer 255
		jmp		ne,	GameFadeOut			; If it's not equal (ne) to 255, jump back to the beginning of the routine
		ret								; Return
; ===========================================================================	
		include	source/SYSCALL.ASM
; ---------------------------------------------------------------------------
		include	source/FUNCS.ASM
; ---------------------------------------------------------------------------
	end