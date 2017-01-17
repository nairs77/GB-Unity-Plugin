using UnityEngine;
using System.Collections;

/**
 * @brief Specify SessionState
 * @author chujinnoon@GB.com
 */

public enum SessionState {
		
	/**
	 * @var ACCESS 세션 API 접근
	 * @var ACCESS_FAILED 세션 API 호출 실패
	 * @var OPEN 로그인 성공
	 * @var TOKEN_REISSUED 토큰 갱신
	 * @var CLOSED 로그아웃 성공
	 * @var Unregister 게임 탈퇴 성공
	 * @var JOIN 회원 가입 성공
	 */
	ACCESS,
	ACCESS_FAILED,
	OPEN,
	TOKEN_REISSUED,
	CLOSED,
	Unregister,
	JOIN
}
