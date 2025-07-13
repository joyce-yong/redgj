using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public Transform airplane;
    public float moveSpeed = 5f;
    public float zOffset = 10f;
    public AudioSource moveSound;

    private Vector3 targetPos;
    private bool isMoving = false;
    private string sceneToLoad;

    void Update()
    {
        if (isMoving)
        {
            if (!moveSound.isPlaying)
                moveSound.Play();

            airplane.position = Vector3.MoveTowards(airplane.position, targetPos, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(airplane.position, targetPos) < 0.1f)
            {
                isMoving = false;
                moveSound.Stop();
                SceneManager.LoadScene(sceneToLoad);


            }
        }
        else
        {
            if (moveSound.isPlaying)
                moveSound.Stop();
        }
    }

    public void OnLevelButtonClicked(string sceneName)
    {
        Vector3 screenPos = Input.mousePosition;

        float distance = Mathf.Abs(Camera.main.transform.position.z - airplane.position.z);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, distance));

        worldPos.z = airplane.position.z;

        targetPos = worldPos;
        sceneToLoad = sceneName;
        isMoving = true;
    }
}