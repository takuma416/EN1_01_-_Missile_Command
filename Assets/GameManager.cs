using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Camera mainCamera_;

    [SerializeField, Header("Prefabs")]
    private Explosion explosionPrefab_;

    [SerializeField]
    private Meteor meteorPrefab_;

    [SerializeField, Header("MeteorSpawner")]
    private BoxCollider2D ground_;
    [SerializeField]
    private float meteorInterval_ = 1;
    private float meteorTimer_;

    [SerializeField]
    private List<Transform> spawnPositions_;

    public void AddScore(int point) { }

    public void Damage(int point) { }

    private void Start()
    {
        GameObject mainCameraObject =
            GameObject.FindGameObjectWithTag("MainCamera");
        bool isGetComponent =
            mainCameraObject.TryGetComponent(out mainCamera_);
        Assert.IsTrue(isGetComponent,
            "MainCameraにCameraコンポーネントがありません");
        Assert.IsTrue(
          spawnPositions_.Count > 0,
          "spawnPositions_に要素が一つもありません。"
        );
        foreach (Transform t in spawnPositions_)
        {
            Assert.IsNotNull(
             t,
             "spawnPositions_にNullが含まれています"
           );
        }
    }

    private void UpdateMeteorTimer() {
        meteorTimer_ -= Time.deltaTime;
        if (meteorTimer_ > 0) { return; }
        meteorTimer_ += meteorInterval_;
        GenerateMeteor();
    }

    private void GenerateMeteor() {
        int max = spawnPositions_.Count;
        int posIndex = Random.Range(0, max);
        Vector3 spawnPosition =
          spawnPositions_[posIndex].position;
       // Vector3 spawnPosition = new Vector3(0, 6, 0);
        Meteor meteor =
          Instantiate(meteorPrefab_,
          spawnPosition, Quaternion.identity);
        meteor.Setup(ground_, this, explosionPrefab_);
    }


    private void GenerateExplosion()
    {
        // クリックしたスクリーン座標の取得し、ワールド座標に変換する
        Vector3 clickPosition =
          mainCamera_.ScreenToWorldPoint(Input.mousePosition);
        clickPosition.z = 0;

        // クリックした座標に爆発を生成
        Explosion explosion = Instantiate(
          explosionPrefab_,
          clickPosition,
          Quaternion.identity
        );
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) { GenerateExplosion(); }

        UpdateMeteorTimer();
    }
}
