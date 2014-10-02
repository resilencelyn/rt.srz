#ifndef _IL_TYPES_H_
#define _IL_TYPES_H_

typedef unsigned char IL_BYTE;
typedef unsigned short IL_WORD;
typedef unsigned int IL_DWORD;
typedef int IL_INT;
typedef char IL_CHAR; 

typedef IL_WORD IL_RETCODE;
typedef IL_DWORD IL_TAG;

typedef void* IL_HANDLE_READER; 
typedef void* IL_READER_SETTINGS;
typedef void* IL_HANDLE_CRYPTO;

#ifdef __cplusplus
    #define IL_FUNC  extern "C" 
#else
    #define IL_FUNC
#endif

#endif