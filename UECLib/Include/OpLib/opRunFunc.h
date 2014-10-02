/** ���������� ���� ��������� �������
  */
#ifndef __OP_RUN_H_
#define __OP_RUN_H_ 

#include "OpRunA0.h" //+++

#ifdef __cplusplus
extern "C" {
#endif

/**  ��������� � �������� ��������� ���������� ��� ���������
  */
IL_FUNC IL_WORD opRunInitialize (s_opContext *p_opContext);

/**  �������������� ���������� �������� ��� �������� ���������� ������� �������
  *  ������������� �������� �������������� ������������ ���������, ����������� ��������� �������:
  * � ������ ������������� ������ ������� dpRunInterrupt � ��������� � �������� ��������� ������
  * � ���� �� ������� �������� ��������������� ��� ������������ � ����������� �������������� �������������� 
  * ��������. ��� ������������� �������������� �������� ������� ���������� ��������������� ����������, � ��
  * ���������� �� ������� ���������.
  *  in_InterruptEvent - �������-���������� 
  * @return ���������� �������-���������� ��� �������-���������� "������������ ��������".
  */
IL_FUNC IL_WORD opRunInterrupt(s_opContext *p_opContext, IL_WORD in_InterruptEvent);

/**  ��������� ������� ������ �� �������� �� ��������� ��� ���������
  *  in_OldState - �������� ����������� ��������� (����� ������ Switch)
  *  inout_JustEntry - ���� ���������� ������ �������
  *  in_Event - ���������� ������� �������� (*inout_Event)
  *  ��� ������ ������ ������� ������ �������� ������������ 0 (����� �� ���������).
  * @return 0 - ����� �� ���������
  * @return >0 - ����� ���������
  */
IL_FUNC IL_BYTE opRunTestExitCondition (s_opContext *p_opContext, IL_WORD in_OldState, IL_WORD in_Event, IL_BYTE *inout_JustEntry);

/**  ������ ��������������� �������. 
  *  in_ExceptionEvent - ����������
  *  ��������� ����������� ���������� (���������������) ���������� 
  * ���������� � ������� ������� IS_CATCHED_EXCEPTION.
  */
IL_FUNC IL_WORD opRunCatchException(s_opContext *p_opContext, IL_WORD in_ExceptionEvent);

/**  �������������� �������������� �������� � ��������
  *  in_ExceptionEvent - ����������
  *  in_wResultCode - ��� ����������� ������������� �������������� ��������
  *  ���������� ������������� ������ � ��� ������, �����
  * ����� ������ ��������� �� ����� ��� ���������� ����������� ��������.
  * ��� ���� ���� �������������� ������� �������������� �����
  * ����������� (ResultCode), ������� ����������� � ��������� �������
  * ��� ������������ ������� ��� �� ������� ������� �������.
  * ����� ������� ������������� ���� ResultCode ��������� 
  * ������ ����������� ������ ��� ������������� ��������������
  * ��������. in_wResultCode - ����� ���� ����� 0!
  * @return ���������� ����������
  */
IL_FUNC IL_WORD opRunThrowException(s_opContext *p_opContext, IL_WORD in_ExceptionEvent, IL_WORD in_wResultCode);

/**  ���������� �������, ���������� RETURN_MARK
  *  ������ ������� ����� ���������� �������� � ����������� �������
  * ��� ��� ���������. ��. ������ IS_RETURN_EVENT, IS_EVENT_RETURN_MARKED
  */
IL_FUNC IL_WORD opRunReturnEvent (IL_WORD in_Event);

/**  ����������� ������ �������� �������� � ���������
  * @return 0 - ������
  * @return >0 - �������
  */
IL_FUNC IL_BYTE opRunIncreaseIndex (s_opContext *p_opContext);

/**  ���������  ������ �������� �������� � ���������
  */
IL_FUNC void opRunDecreaseIndex (s_opContext *p_opContext);

// ��������������� ��������
IL_FUNC IL_WORD opRunAuthOperationA1 (s_opContext *p_opContext, IL_WORD *inout_Event);
IL_FUNC IL_WORD opRunEstablishIssuerSessionA1 (s_opContext *p_opContext, IL_WORD *inout_Event);
IL_FUNC IL_WORD opRunAuthApplicationA1 (s_opContext *p_opContext, IL_WORD *inout_Event);



#ifdef __cplusplus
}
#endif

#endif//__OP_RUN_H_ 
