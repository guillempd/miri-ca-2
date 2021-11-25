using UnityEngine;

public class CrowdGenerator : MonoBehaviour
{
    [SerializeField] private GameObject m_AgentPrefab;
    [SerializeField] private int m_NumAgents = 100;
    [SerializeField] private int m_NumCellsX = 20;
    [SerializeField] private int m_NumCellsZ = 20;

    private void Start()
    {
        Vector3 scale = new Vector3(m_NumCellsX, 1, m_NumCellsZ);
        transform.localScale = scale;

        bool[,] cellIsFree = new bool[m_NumCellsX, m_NumCellsZ];
        for (int x = 0; x < m_NumCellsX; ++x)
            for (int z = 0; z < m_NumCellsZ; ++z)
                cellIsFree[x, z] = true;

        for (int i = 0; i < m_NumAgents; ++i)
        {
            int cellX = Random.Range(0, m_NumCellsX);
            int cellZ = Random.Range(0, m_NumCellsZ);

            while (!cellIsFree[cellX, cellZ])
            {
                cellX = Random.Range(0, m_NumCellsX);
                cellZ = Random.Range(0, m_NumCellsZ);
            }

            float cellOffsetX = Random.Range(-0.5f, 0.5f);
            float cellOffsetZ = Random.Range(-0.5f, 0.5f);

            Vector3 position = new Vector3(2*cellX + 1, 0, 2*cellZ + 1) + new Vector3(cellOffsetX, 0, cellOffsetZ);

            Instantiate(m_AgentPrefab, position, Quaternion.identity);

            cellIsFree[cellX, cellZ] = false;
        }
    }

}
