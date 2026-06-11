# 구현 기능

## 플레이어

- Unity Input System을 사용하여 플레이어 이동 구현
- Rigidbody 기반 이동 및 점프 시스템 구현
- Raycast를 사용한 바닥 감지 처리

## 몬스터 AI

- 플레이어 감지 범위 및 공격 범위 구현
- FSM(Finite State Machine) 기반 몬스터 상태 관리
  - Idle
  - Chase
  - Attack

## 전투 시스템

- sqrMagnitude를 사용한 거리 계산 최적화
- 범위 공격 시 공격 범위 내 몬스터 넉백 구현

## 스테이지 시스템

- List 자료구조를 사용하여 현재 스테이지 몬스터 관리
- StageManager에서 `List<Monster>`를 통해 살아있는 몬스터 추적
- 몬스터 사망 시 이벤트 기반으로 스테이지 진행 처리

## 길찾기 시스템

- 플레이어와 가장 가까운 몬스터 탐색 기능 구현
- RoomNode 기반 A* 경로 탐색 구현
- Gizmo를 활용한 경로 시각화