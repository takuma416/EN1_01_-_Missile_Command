using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float maxLifeTime_ = 1;
    private float time_ = 0;

    private void Start()
    {
        time_ = maxLifeTime_;
    }

    void Update()
    {
        time_ -= Time.deltaTime;
        if (time_ > 0) { return; }
        Destroy(gameObject);
    }

}
