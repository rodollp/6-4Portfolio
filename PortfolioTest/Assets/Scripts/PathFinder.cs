using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    //RoomNode에 있는 것을 리스트화
    public List<RoomNode> rooms = new();

    // 플레이어가 있는 룸 노드
    RoomNode playerRoom;
    // 몬스터가 있는 룸 노드
    RoomNode targetRoom;

    // 플레이어 위치
    [SerializeField] Transform player;
    //스테이지 매니져로 살아있는 몬스터 추적
    [SerializeField] StageManager stageManager;

    // playerRoom에서 targetRoom까지 이어지는 경로 저장 리스트
    List<RoomNode> path = new();

    private void Awake()
    {
        //현재 씬에 존재하는 RoomNode 컴포넌트를 찾아서 배열로 가져옴
        RoomNode[] found = FindObjectsByType<RoomNode>(FindObjectsSortMode.None);
        //rooms에 found로 채워넣기
        rooms.AddRange(found);

        Debug.Log($"수집된 RoomNode 수: {rooms.Count}");
    }

    private void Update()
    {
        UpdateRooms();
        FindPath();
    }

    // 플레이어랑 몬스터 위치에서 가장 가까운 RoomNode를 갱신하는 메소드
    void UpdateRooms()
    {
        // 플레이어 위치 기준으로 현재 방 찾기
        playerRoom = FindClosestRoom(player.position);

        // 현재 살아있는 몬스터 중 가장 가까운 몬스터 찾기
        MonsterStatus target = stageManager.ShortMagnitude(player.position);

        if (target == null)
        {
            targetRoom = null;
            path.Clear();
            return;
        }

        // 몬스터 위치 기준으로 목표 방 찾기
        targetRoom = FindClosestRoom(target.transform.position);
    }

    // 가까운 룸 찾기
    RoomNode FindClosestRoom(Vector3 position)
    {
        //가장 가까운 룸 노드를 저장할 공간
        RoomNode closest = null;
        // 무조건 첫번째 방이 closest으로 저장되게 하려고 최대값을 사용
        float closestDist = float.MaxValue;

        // List<RoomNode> rooms에서 하나씩 꺼내며 검사
        foreach (RoomNode room in rooms)
        {
            // room의 위치에서 전달받은 position 사이의 거리를 제곱하여 나타낸 dist
            float dist =(room.transform.position - position).sqrMagnitude;

            // 현재 방이 지금까지 찾은 방보다 더 가까우면
            // closestDist를 그 거리로 갱신하고 closest를 현재 room으로 바꾼다
            if (dist < closestDist)
            {
                closestDist = dist;
                closest = room;
            }
        }

        return closest;
    }

    void FindPath()
    {
        path.Clear();

        if (playerRoom == null || targetRoom == null)
            return;
        //검사를 진행할 노드
        List<RoomNode> openList = new List<RoomNode>();
        //검사가 끝난 노드
        HashSet<RoomNode> closedList = new HashSet<RoomNode>();
        // 지금까지 지나왔던 노드를 저장
        Dictionary<RoomNode, RoomNode> cameFrom = new Dictionary<RoomNode, RoomNode>();
        //시작방에서 현재까지의 이동 비용
        Dictionary<RoomNode, float> gCost = new Dictionary<RoomNode, float>();
        //Gcost + 목표까지의 예상 비용
        Dictionary<RoomNode, float> fCost = new Dictionary<RoomNode, float>();

        openList.Add(playerRoom);
        gCost[playerRoom] = 0;
        fCost[playerRoom] = Heuristic(playerRoom, targetRoom);

        while (openList.Count > 0)
        {
            RoomNode current = GetLowestFCostNode(openList, fCost);

            if (current == targetRoom)
            {
                BuildPath(cameFrom, current);
                return;
            }

            openList.Remove(current);
            closedList.Add(current);

            foreach (RoomNode next in current.GetNeighbors())
            {
                if (closedList.Contains(next))
                    continue;

                float newGCost = gCost[current] + Vector3.Distance(current.transform.position,next.transform.position);

                if (!gCost.ContainsKey(next) || newGCost < gCost[next])
                {
                    cameFrom[next] = current;
                    gCost[next] = newGCost;
                    fCost[next] = newGCost + Heuristic(next, targetRoom);

                    if (!openList.Contains(next))
                        openList.Add(next);
                }
            }
        }
    }
    float Heuristic(RoomNode a, RoomNode b)
    {
        return Vector3.Distance(a.transform.position,b.transform.position
        );
    }

    RoomNode GetLowestFCostNode(List<RoomNode> openList,Dictionary<RoomNode, float> fCost)
    {
        RoomNode bestNode = openList[0];
        float bestCost = fCost[bestNode];

        foreach (RoomNode node in openList)
        {
            if (fCost[node] < bestCost)
            {
                bestNode = node;
                bestCost = fCost[node];
            }
        }

        return bestNode;
    }

    void BuildPath(Dictionary<RoomNode, RoomNode> cameFrom,RoomNode current)
    {
        path.Clear();

        path.Add(current);

        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Add(current);
        }

        path.Reverse();
    }
    private void OnDrawGizmos()
    {
        if (path == null || path.Count <= 1)
            return;

        Gizmos.color = Color.red;

        for (int i = 0; i < path.Count - 1; i++)
        {
            Gizmos.DrawLine(
                path[i].transform.position + Vector3.up * 2f,
                path[i + 1].transform.position + Vector3.up * 2f
            );
        }
    }
}