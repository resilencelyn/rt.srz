#ifndef _KEYTYPE_H_
#define _KEYTYPE_H_

// ПИН1 - Для верификация гражданина
#define  IL_KEYTYPE_01_PIN				0x01
// ПИН2 - Для ЭЦП держателя карты
#define  IL_KEYTYPE_02_PIN				0x02
// ПИН3 - Не используется
#define  IL_KEYTYPE_03_PIN				0x03
// ПИН4 - Не используется				
#define  IL_KEYTYPE_04_PIN				0x04
// КРП - Для разблокировки ПИН
#define  IL_KEYTYPE_05_PUK				0x05

#define  IL_KEYTYPE_15_P_CA_ID_GOST		0x15
#define  IL_KEYTYPE_16_P_CA_ID_RSA		0x16
#define  IL_KEYTYPE_17_IC_ID_GOST		0x17
#define  IL_KEYTYPE_18_IC_ID_RSA		0x18
#define  IL_KEYTYPE_29_MK_IC_ID_GOST	0x29
#define  IL_KEYTYPE_2A_MK_IC_ID_DES		0x2A
#define  IL_KEYTYPE_2B_MK_SM_ID_GOST	0x2B
#define  IL_KEYTYPE_2C_MK_SM_ID_DES		0x2C

#endif