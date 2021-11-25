using UnityEngine;

public class CrowdSimulation : MonoBehaviour
{
    [SerializeField] private Agent m_AgentPrefab;
    [SerializeField] private int m_NumAgents = 100;
    [SerializeField] private int m_NumCellsX = 20;
    [SerializeField] private int m_NumCellsZ = 20;

    private Agent[] m_Agents;
    private Vector3[] m_AgentsGoalPosition;


    private void Start()
    {
        m_Agents = new Agent[m_NumAgents];
        m_AgentsGoalPosition = new Vector3[m_NumAgents];

        Vector3 scale = new Vector3(m_NumCellsX, 1, m_NumCellsZ);
        transform.localScale = scale;

        bool[,] freeCells = new bool[m_NumCellsX, m_NumCellsZ];
        for (int x = 0; x < m_NumCellsX; ++x)
            for (int z = 0; z < m_NumCellsZ; ++z)
                freeCells[x, z] = true;

        for (int i = 0; i < m_NumAgents; ++i)
        {
            m_Agents[i] = InstantiateAgent(freeCells);
            m_Agents[i].SetGoalPosition(RandomGoalPosition());
            
        }
    }

    private Agent InstantiateAgent(bool[,] freeCells)
    {
        int cellX = Random.Range(0, m_NumCellsX);
        int cellZ = Random.Range(0, m_NumCellsZ);
        while (!freeCells[cellX, cellZ])
        {
            cellX = Random.Range(0, m_NumCellsX);
            cellZ = Random.Range(0, m_NumCellsZ);
        }
        freeCells[cellX, cellZ] = false;

        float cellOffsetX = Random.Range(-0.5f, 0.5f);
        float cellOffsetZ = Random.Range(-0.5f, 0.5f);

        Vector3 position = new Vector3(2 * cellX + 1, 0, 2 * cellZ + 1) + new Vector3(cellOffsetX, 0, cellOffsetZ);

        return Instantiate(m_AgentPrefab, position, Quaternion.identity);
    }

    private void Update()
    {
        for (int i = 0; i < m_NumAgents; ++i)
        {
            Agent agent = m_Agents[i];
            if (agent.GoalReached()) agent.SetGoalPosition(RandomGoalPosition());
        }
    }

    private Vector3 RandomGoalPosition()
    {
        return new Vector3(Random.Range(0.0f, 2 * m_NumCellsX), 0.0f, Random.Range(0.0f, 2 * m_NumCellsZ));
    }

}
