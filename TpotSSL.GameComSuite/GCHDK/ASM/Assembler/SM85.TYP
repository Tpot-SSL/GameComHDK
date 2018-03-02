#################################################
#						#
#		SM85.TYP			#
#						#
#		Last changed: 1996/02/15	#
#################################################
#
# Definition of SM8500 Series
#
begin	8500
	rom		efff
	ram		7ff
	ramstart	0
	max_io		7f
#
	ie	rr	10
	ie0	p	10
	ie1	p	11
	if	rr	12
	if0	p	12
	if1	p	13
	exin	p	14
	src	p	15
	srd	p	16
	adcd	p	17
	adcc	r	18
	sys	r	19
	ckkc	r	1a
	tstreg	r	1b
	sp	rr	1c
	sph	r	1c
	spl	r	1d
	ps	rr	1e
	ps0	r	1e
	ps1	r	1f
	p0	r	20
	p1	r	21
	p2	r	22
	p3	r	23
	p4	r	24
	p5	r	25
	p6	r	26
	p7	r	27
	p8	r	28
	p9	r	29
	pa	r	2a
	urtt	r	2b
	urtr	r	2c
	urts	r	2d
	urtc	r	2e
	urtb	r	2f
	p0c	r	30
	p1c	r	31
	p2c	r	32
	p3c	r	33
	p4c	r	34
	p1pc	r	35
	p2pc	r	36
	p7c	r	37
	p8c	r	38
	p9c	r	39
	p5c	r	3a
	ppc	r	3b
	wgsd0	r	3c
	wgsd1	r	3d
	wgc	r	3e
	dacc	r	3f
	tm0	rr	40
	tm0h	r	40
	tm0l	r	41
	tm0m	rr	42
	tm0mh	r	42
	tm0ml	r	43
	tm0p	rr	44
	tm0ph	r	44
	tm0pl	r	45
	tm0c	rr	46
	tm0c0	r	46
	tm0c1	r	47
	tm1	r	48
	tm1m	r	49
	tm1c	r	4a
	tm2	r	4b
	tm2m	r	4c
	tm2c	r	4d
	tm12ct	r	4e
	tm43	rr	50
	tm4	r	50
	tm3	r	51
	tm3c	r	52
	tm43m	rr	54
	tm4m	r	54
	tm3m	r	55
	tm4c	r	56
	tm5	r	58
	tm5m	r	59
	tm5c	r	5a
	tm6	r	5c
	tm6c	r	5d
	wdt	r	5e
	wdtc	r	5f
	wgm00	r	60
	wgm01	r	61
	wgm02	r	62
	wgm03	r	63
	wgm04	r	64
	wgm05	r	65
	wgm06	r	66
	wgm07	r	67
	wgm08	r	68
	wgm09	r	69
	wgm0a	r	6a
	wgm0b	r	6b
	wgm0c	r	6c
	wgm0d	r	6d
	wgm0e	r	6e
	wgm0f	r	6f
	wgm10	r	70
	wgm11	r	71
	wgm12	r	72
	wgm13	r	73
	wgm14	r	74
	wgm15	r	75
	wgm16	r	76
	wgm17	r	77
	wgm18	r	78
	wgm19	r	79
	wgm1a	r	7a
	wgm1b	r	7b
	wgm1c	r	7c
	wgm1d	r	7d
	wgm1e	r	7e
	wgm1f	r	7f
end
#
# Difinition of SM8502
#
begin	8502
	rom		5fff
	ram		3ff
	ramstart	0
	max_io		7f
#
	ie	rr	10
	ie0	p	10
	ie1	p	11
	if	rr	12
	if0	p	12
	if1	p	13
	exin	p	14
	src	p	15
	srd	p	16
	adcd	p	17
#
	adcc	r	18
	sys	r	19
	ckkc	r	1a
#	tstreg	r	1b
	sp	rr	1c
	sph	r	1c
	spl	r	1d
	ps	rr	1e
	ps0	r	1e
	ps1	r	1f
#
	p0	r	20
	p1	r	21
	p2	r	22
	p3	r	23
	p4	r	24
	p5	r	25
	p6	r	26
	p7	r	27
	p8	r	28
	p9	r	29
	pa	r	2a
	urtt	r	2b
	urtr	r	2c
	urts	r	2d
	urtc	r	2e
	urtb	r	2f
	p0c	r	30
	p1c	r	31
	p2c	r	32
	p3c	r	33
	p4c	r	34
	p1pc	r	35
	p2pc	r	36
	p7c	r	37
	p8c	r	38
	p9c	r	39
	p5c	r	3a
	ppc	r	3b
	wgsd0	r	3c
	wgsd1	r	3d
	wgc	r	3e
	dacc	r	3f
#
	tm0	rr	40
	tm0h	r	40
	tm0l	r	41
	tm0m	rr	42
	tm0mh	r	42
	tm0ml	r	43
	tm0p	rr	44
	tm0ph	r	44
	tm0pl	r	45
	tm0c	rr	46
	tm0c0	r	46
	tm0c1	r	47
	tm1	r	48
	tm1m	r	49
	tm1c	r	4a
	tm2	r	4b
	tm2m	r	4c
	tm2c	r	4d
	tm12ct	r	4e
	tm43	rr	50
	tm4	r	50
	tm3	r	51
	tm3c	r	52
	tm43m	rr	54
	tm4m	r	54
	tm3m	r	55
	tm4c	r	56
	tm5	r	58
	tm5m	r	59
	tm5c	r	5a
	tm6	r	5c
	tm6c	r	5d
	wdt	r	5e
	wdtc	r	5f
	wgm00	r	60
	wgm01	r	61
	wgm02	r	62
	wgm03	r	63
	wgm04	r	64
	wgm05	r	65
	wgm06	r	66
	wgm07	r	67
	wgm08	r	68
	wgm09	r	69
	wgm0a	r	6a
	wgm0b	r	6b
	wgm0c	r	6c
	wgm0d	r	6d
	wgm0e	r	6e
	wgm0f	r	6f
	wgm10	r	70
	wgm11	r	71
	wgm12	r	72
	wgm13	r	73
	wgm14	r	74
	wgm15	r	75
	wgm16	r	76
	wgm17	r	77
	wgm18	r	78
	wgm19	r	79
	wgm1a	r	7a
	wgm1b	r	7b
	wgm1c	r	7c
	wgm1d	r	7d
	wgm1e	r	7e
	wgm1f	r	7f
end
#
# Difinition of SM8503
#
begin	8503
	rom		7fff
	ram		3ff
	ramstart	0
	max_io		7f
#
	ie	rr	10
	ie0	p	10
	ie1	p	11
	if	rr	12
	if0	p	12
	if1	p	13
	exin	p	14
	src	p	15
	srd	p	16
	adcd	p	17
#
	adcc	r	18
	sys	r	19
	ckkc	r	1a
#	tstreg	r	1b
	sp	rr	1c
	sph	r	1c
	spl	r	1d
	ps	rr	1e
	ps0	r	1e
	ps1	r	1f
#
	p0	r	20
	p1	r	21
	p2	r	22
	p3	r	23
	p4	r	24
	p5	r	25
	p6	r	26
	p7	r	27
	p8	r	28
	p9	r	29
	pa	r	2a
	urtt	r	2b
	urtr	r	2c
	urts	r	2d
	urtc	r	2e
	urtb	r	2f
	p0c	r	30
	p1c	r	31
	p2c	r	32
	p3c	r	33
	p4c	r	34
	p1pc	r	35
	p2pc	r	36
	p7c	r	37
	p8c	r	38
	p9c	r	39
	p5c	r	3a
	ppc	r	3b
	wgsd0	r	3c
	wgsd1	r	3d
	wgc	r	3e
	dacc	r	3f
#
	tm0	rr	40
	tm0h	r	40
	tm0l	r	41
	tm0m	rr	42
	tm0mh	r	42
	tm0ml	r	43
	tm0p	rr	44
	tm0ph	r	44
	tm0pl	r	45
	tm0c	rr	46
	tm0c0	r	46
	tm0c1	r	47
	tm1	r	48
	tm1m	r	49
	tm1c	r	4a
	tm2	r	4b
	tm2m	r	4c
	tm2c	r	4d
	tm12ct	r	4e
	tm43	rr	50
	tm4	r	50
	tm3	r	51
	tm3c	r	52
	tm43m	rr	54
	tm4m	r	54
	tm3m	r	55
	tm4c	r	56
	tm5	r	58
	tm5m	r	59
	tm5c	r	5a
	tm6	r	5c
	tm6c	r	5d
	wdt	r	5e
	wdtc	r	5f
	wgm00	r	60
	wgm01	r	61
	wgm02	r	62
	wgm03	r	63
	wgm04	r	64
	wgm05	r	65
	wgm06	r	66
	wgm07	r	67
	wgm08	r	68
	wgm09	r	69
	wgm0a	r	6a
	wgm0b	r	6b
	wgm0c	r	6c
	wgm0d	r	6d
	wgm0e	r	6e
	wgm0f	r	6f
	wgm10	r	70
	wgm11	r	71
	wgm12	r	72
	wgm13	r	73
	wgm14	r	74
	wgm15	r	75
	wgm16	r	76
	wgm17	r	77
	wgm18	r	78
	wgm19	r	79
	wgm1a	r	7a
	wgm1b	r	7b
	wgm1c	r	7c
	wgm1d	r	7d
	wgm1e	r	7e
	wgm1f	r	7f
end
#
# Difinition of SM8504
#
begin	8504
	rom		9fff
	ram		3ff
	ramstart	0
	max_io		7f
#
	ie	rr	10
	ie0	p	10
	ie1	p	11
	if	rr	12
	if0	p	12
	if1	p	13
	exin	p	14
	src	p	15
	srd	p	16
	adcd	p	17
	adcc	r	18
	sys	r	19
	ckkc	r	1a
	tstreg	r	1b
	sp	rr	1c
	sph	r	1c
	spl	r	1d
	ps	rr	1e
	ps0	r	1e
	ps1	r	1f
	p0	r	20
	p1	r	21
	p2	r	22
	p3	r	23
	p4	r	24
	p5	r	25
	p6	r	26
	p7	r	27
	p8	r	28
	p9	r	29
	pa	r	2a
	urtt	r	2b
	urtr	r	2c
	urts	r	2d
	urtc	r	2e
	urtb	r	2f
	p0c	r	30
	p1c	r	31
	p2c	r	32
	p3c	r	33
	p4c	r	34
	p1pc	r	35
	p2pc	r	36
	p7c	r	37
	p8c	r	38
	p9c	r	39
	p5c	r	3a
	ppc	r	3b
	wgsd0	r	3c
	wgsd1	r	3d
	wgc	r	3e
	dacc	r	3f
	tm0	rr	40
	tm0h	r	40
	tm0l	r	41
	tm0m	rr	42
	tm0mh	r	42
	tm0ml	r	43
	tm0p	rr	44
	tm0ph	r	44
	tm0pl	r	45
	tm0c	rr	46
	tm0c0	r	46
	tm0c1	r	47
	tm1	r	48
	tm1m	r	49
	tm1c	r	4a
	tm2	r	4b
	tm2m	r	4c
	tm2c	r	4d
	tm12ct	r	4e
	tm43	rr	50
	tm4	r	50
	tm3	r	51
	tm3c	r	52
	tm43m	rr	54
	tm4m	r	54
	tm3m	r	55
	tm4c	r	56
	tm5	r	58
	tm5m	r	59
	tm5c	r	5a
	tm6	r	5c
	tm6c	r	5d
	wdt	r	5e
	wdtc	r	5f
	wgm00	r	60
	wgm01	r	61
	wgm02	r	62
	wgm03	r	63
	wgm04	r	64
	wgm05	r	65
	wgm06	r	66
	wgm07	r	67
	wgm08	r	68
	wgm09	r	69
	wgm0a	r	6a
	wgm0b	r	6b
	wgm0c	r	6c
	wgm0d	r	6d
	wgm0e	r	6e
	wgm0f	r	6f
	wgm10	r	70
	wgm11	r	71
	wgm12	r	72
	wgm13	r	73
	wgm14	r	74
	wgm15	r	75
	wgm16	r	76
	wgm17	r	77
	wgm18	r	78
	wgm19	r	79
	wgm1a	r	7a
	wgm1b	r	7b
	wgm1c	r	7c
	wgm1d	r	7d
	wgm1e	r	7e
	wgm1f	r	7f
end
#
# Difinition of SM8505
#
begin	8505
	rom		bfff
	ram		7ff
	ramstart	0
	max_io		7f
#
	ie	rr	10
	ie0	p	10
	ie1	p	11
	if	rr	12
	if0	p	12
	if1	p	13
	exin	p	14
	src	p	15
	srd	p	16
	adcd	p	17
#
	adcc	r	18
	sys	r	19
	ckkc	r	1a
#	tstreg	r	1b
	sp	rr	1c
	sph	r	1c
	spl	r	1d
	ps	rr	1e
	ps0	r	1e
	ps1	r	1f
#
	p0	r	20
	p1	r	21
	p2	r	22
	p3	r	23
	p4	r	24
	p5	r	25
	p6	r	26
	p7	r	27
	p8	r	28
	p9	r	29
	pa	r	2a
	urtt	r	2b
	urtr	r	2c
	urts	r	2d
	urtc	r	2e
	urtb	r	2f
	p0c	r	30
	p1c	r	31
	p2c	r	32
	p3c	r	33
	p4c	r	34
	p1pc	r	35
	p2pc	r	36
	p7c	r	37
	p8c	r	38
	p9c	r	39
	p5c	r	3a
	ppc	r	3b
	wgsd0	r	3c
	wgsd1	r	3d
	wgc	r	3e
	dacc	r	3f
#
	tm0	rr	40
	tm0h	r	40
	tm0l	r	41
	tm0m	rr	42
	tm0mh	r	42
	tm0ml	r	43
	tm0p	rr	44
	tm0ph	r	44
	tm0pl	r	45
	tm0c	rr	46
	tm0c0	r	46
	tm0c1	r	47
	tm1	r	48
	tm1m	r	49
	tm1c	r	4a
	tm2	r	4b
	tm2m	r	4c
	tm2c	r	4d
	tm12ct	r	4e
	tm43	rr	50
	tm4	r	50
	tm3	r	51
	tm3c	r	52
	tm43m	rr	54
	tm4m	r	54
	tm3m	r	55
	tm4c	r	56
	mcf	r	57
	tm5	r	58
	tm5m	r	59
	tm5c	r	5a
	tm6	r	5c
	tm6c	r	5d
	wdt	r	5e
	wdtc	r	5f
	wgm00	r	60
	wgm01	r	61
	wgm02	r	62
	wgm03	r	63
	wgm04	r	64
	wgm05	r	65
	wgm06	r	66
	wgm07	r	67
	wgm08	r	68
	wgm09	r	69
	wgm0a	r	6a
	wgm0b	r	6b
	wgm0c	r	6c
	wgm0d	r	6d
	wgm0e	r	6e
	wgm0f	r	6f
	wgm10	r	70
	wgm11	r	71
	wgm12	r	72
	wgm13	r	73
	wgm14	r	74
	wgm15	r	75
	wgm16	r	76
	wgm17	r	77
	wgm18	r	78
	wgm19	r	79
	wgm1a	r	7a
	wgm1b	r	7b
	wgm1c	r	7c
	wgm1d	r	7d
	wgm1e	r	7e
	wgm1f	r	7f
end
#
# Difinition of SM8506
#
begin	8506
	rom		efff
	ram		7ff
	ramstart	0
	max_io		7f
#
	ie	rr	10
	ie0	p	10
	ie1	p	11
	if	rr	12
	if0	p	12
	if1	p	13
	exin	p	14
	src	p	15
	srd	p	16
	adcd	p	17
#
	adcc	r	18
	sys	r	19
	ckkc	r	1a
#	tstreg	r	1b
	sp	rr	1c
	sph	r	1c
	spl	r	1d
	ps	rr	1e
	ps0	r	1e
	ps1	r	1f
#
	p0	r	20
	p1	r	21
	p2	r	22
	p3	r	23
	p4	r	24
	p5	r	25
	p6	r	26
	p7	r	27
	p8	r	28
	p9	r	29
	pa	r	2a
	urtt	r	2b
	urtr	r	2c
	urts	r	2d
	urtc	r	2e
	urtb	r	2f
	p0c	r	30
	p1c	r	31
	p2c	r	32
	p3c	r	33
	p4c	r	34
	p1pc	r	35
	p2pc	r	36
	p7c	r	37
	p8c	r	38
	p9c	r	39
	p5c	r	3a
	ppc	r	3b
	wgsd0	r	3c
	wgsd1	r	3d
	wgc	r	3e
	dacc	r	3f
#
	tm0	rr	40
	tm0h	r	40
	tm0l	r	41
	tm0m	rr	42
	tm0mh	r	42
	tm0ml	r	43
	tm0p	rr	44
	tm0ph	r	44
	tm0pl	r	45
	tm0c	rr	46
	tm0c0	r	46
	tm0c1	r	47
	tm1	r	48
	tm1m	r	49
	tm1c	r	4a
	tm2	r	4b
	tm2m	r	4c
	tm2c	r	4d
	tm12ct	r	4e
	tm43	rr	50
	tm4	r	50
	tm3	r	51
	tm3c	r	52
	tm43m	rr	54
	tm4m	r	54
	tm3m	r	55
	tm4c	r	56
	mcf	r	57
	tm5	r	58
	tm5m	r	59
	tm5c	r	5a
	tm6	r	5c
	tm6c	r	5d
	wdt	r	5e
	wdtc	r	5f
	wgm00	r	60
	wgm01	r	61
	wgm02	r	62
	wgm03	r	63
	wgm04	r	64
	wgm05	r	65
	wgm06	r	66
	wgm07	r	67
	wgm08	r	68
	wgm09	r	69
	wgm0a	r	6a
	wgm0b	r	6b
	wgm0c	r	6c
	wgm0d	r	6d
	wgm0e	r	6e
	wgm0f	r	6f
	wgm10	r	70
	wgm11	r	71
	wgm12	r	72
	wgm13	r	73
	wgm14	r	74
	wgm15	r	75
	wgm16	r	76
	wgm17	r	77
	wgm18	r	78
	wgm19	r	79
	wgm1a	r	7a
	wgm1b	r	7b
	wgm1c	r	7c
	wgm1d	r	7d
	wgm1e	r	7e
	wgm1f	r	7f
end
#
# Difinition of LU8500F0/F1
#
begin	85F
	rom		ffff
	ram		7ff
	ramstart	0
	max_io		7f
#
	ie	rr	10
	ie0	p	10
	ie1	p	11
	if	rr	12
	if0	p	12
	if1	p	13
	exin	p	14
	src	p	15
	srd	p	16
	adcd	p	17
#
	adcc	r	18
	sys	r	19
	ckkc	r	1a
#	tstreg	r	1b
	sp	rr	1c
	sph	r	1c
	spl	r	1d
	ps	rr	1e
	ps0	r	1e
	ps1	r	1f
#
	p0	r	20
	p1	r	21
	p2	r	22
	p3	r	23
	p4	r	24
	p5	r	25
	p6	r	26
	p7	r	27
	p8	r	28
	p9	r	29
	pa	r	2a
	urtt	r	2b
	urtr	r	2c
	urts	r	2d
	urtc	r	2e
	urtb	r	2f
	p0c	r	30
	p1c	r	31
	p2c	r	32
	p3c	r	33
	p4c	r	34
	p1pc	r	35
	p2pc	r	36
	p7c	r	37
	p8c	r	38
	p9c	r	39
	p5c	r	3a
	ppc	r	3b
	wgsd0	r	3c
	wgsd1	r	3d
	wgc	r	3e
	dacc	r	3f
#
	tm0	rr	40
	tm0h	r	40
	tm0l	r	41
	tm0m	rr	42
	tm0mh	r	42
	tm0ml	r	43
	tm0p	rr	44
	tm0ph	r	44
	tm0pl	r	45
	tm0c	rr	46
	tm0c0	r	46
	tm0c1	r	47
	tm1	r	48
	tm1m	r	49
	tm1c	r	4a
	tm2	r	4b
	tm2m	r	4c
	tm2c	r	4d
	tm12ct	r	4e
	tm43	rr	50
	tm4	r	50
	tm3	r	51
	tm3c	r	52
#	status	r	53
	tm43m	rr	54
	tm4m	r	54
	tm3m	r	55
	tm4c	r	56
	mcf	r	57
	tm5	r	58
	tm5m	r	59
	tm5c	r	5a
	sys2	r	5b
	tm6	r	5c
	tm6c	r	5d
	wdt	r	5e
	wdtc	r	5f
	wgm00	r	60
	wgm01	r	61
	wgm02	r	62
	wgm03	r	63
	wgm04	r	64
	wgm05	r	65
	wgm06	r	66
	wgm07	r	67
	wgm08	r	68
	wgm09	r	69
	wgm0a	r	6a
	wgm0b	r	6b
	wgm0c	r	6c
	wgm0d	r	6d
	wgm0e	r	6e
	wgm0f	r	6f
	wgm10	r	70
	wgm11	r	71
	wgm12	r	72
	wgm13	r	73
	wgm14	r	74
	wgm15	r	75
	wgm16	r	76
	wgm17	r	77
	wgm18	r	78
	wgm19	r	79
	wgm1a	r	7a
	wgm1b	r	7b
	wgm1c	r	7c
	wgm1d	r	7d
	wgm1e	r	7e
	wgm1f	r	7f
end

#
# Difinition of LU8521 for TIGER
#
begin	8521
	rom		ffff
	ram		7ff
	ramstart	0
	max_io		7f
#
	ie0	p	10
	ie1	p	11
	ir0	p	12
	ir1	p	13
	p0	p	14
	p1	p	15
	p2	p	16
	p3	p	17
#not used		18
	sys	r	19
	ckc	r	1a
#not used		1b
	sp	rr	1c
	sph	r	1c
	spl	r	1d
	ps0	r	1e
	ps1	r	1f
#
	p0c	r	20
	p1c	r	21
	p2c	r	22
	p3c	r	23
	mmu0	r	24
	mmu1	r	25
	mmu2	r	26
	mmu3	r	27
	mmu4	r	28
#not used		29
#not used		2a
	uartt	r	2b
	uartr	r	2c
	uarts	r	2d
	uartc	r	2e
#not used		2f
	lcc	r	30
	lch	r	31
	lcv	r	32
#not used		33
	dmc	r	34
	dmx1	r	35
	dmy1	r	36
	dmdx	r	37
	dmdy	r	38
	dmx2	r	39
	dmy2	r	3a
	dmpl	r	3b
	dmbr	r	3c
	dmvp	r	3d
#not used		3e
#not used		3f
	sgc	r	40
#not used		41
	sg0l	r	42
#not used        	43
	sg1l	r	44
#not used		45
	sg0th	r	46
	sg0tl	r	47
	sg1th	r	48
	sg1tl	r	49
	sg2l	r	4a
#not used		4b
	sg2th	r	4c
	sg2tl	r	4d
	sgda	r	4e
#not used		4f
	tm0c	r	50
	tm0d	r	51
	tm1c	r	52
	tm1d	r	53
	clkt	r	54
#not used		55
#not used		56
#not used		57
#not used		58
#not used		59
#not used		5a
#not used		5b
#not used		5c
#not used		5d
	wdt	r	5e
	wdtc	r	5f
	sg0w0	r	60
	sg0w1	r	61
	sg0w2	r	62
	sg0w3	r	63
	sg0w4	r	64
	sg0w5	r	65
	sg0w6	r	66
	sg0w7	r	67
	sg0w8	r	68
	sg0w9	r	69
	sg0w10	r	6a
	sg0w11	r	6b
	sg0w12	r	6c
	sg0w13	r	6d
	sg0w14	r	6e
	sg0w15	r	6f
	sg1w0	r	70
	sg1w1	r	71
	sg1w2	r	72
	sg1w3	r	73
	sg1w4	r	74
	sg1w5	r	75
	sg1w6	r	76
	sg1w7	r	77
	sg1w8	r	78
	sg1w9	r	79
	sg1w10	r	7a
	sg1w11	r	7b
	sg1w12	r	7c
	sg1w13	r	7d
	sg1w14	r	7e
	sg1w15	r	7f
end

