1. 플레이어 InPutSystem으로 움직임 구현 , Rigidbody와 Raycast를 통한 바닥 확인 및 점프 구현

2. 몬스터의 플레이어 감지 및 인지범위, 공격범위, FSM 구현

3. 플레이어 범위 공격(sqrMagnutude 사용하여 거리측정)으로 몬스터가 범위에 있을시 몬스터 넉백 구현

자료구조로는 List를 사용
StageManager에서 List<MonsterStatus>를 사용해서 현재 스테이지에 생성된 몬스터를 관리.
스테이지에 있는 몬스터와 플레이어 간의 최단거리에 있는 몬스터 찾는 함수 생성 
PlayerAI에서 위의 함수를 이용해 몬스터 위치 경로 탐색 결과 보이게 만들기