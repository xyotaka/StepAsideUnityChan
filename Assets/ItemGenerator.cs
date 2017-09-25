using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour {
    //carPrefabを入れる
    public GameObject carPrefab;
    //coinPrefabを入れる
    public GameObject coinPrefab;
    //conePrefabを入れる
    public GameObject conePrefab;
    //スタート地点
    private int startPos;
    //ゴール地点
    private int goalPos = 120;
    //アイテムを出すx方向の範囲
    private float posRange = 3.4f;

    //unitychanのGameObject
    private GameObject unityChan;
    UnityChanController UCC;

    // Use this for initialization
    void Start () {
        startPos = -160;
        this.unityChan = GameObject.Find("unitychan");
        UCC = unityChan.GetComponent<UnityChanController>();

        //一定の距離ごとにアイテムを生成
        for (int i = startPos; i <= startPos + 45; i += 15)
        {
            CreateItems(i);
        }
    }

    //アイテムを生成する
    void CreateItems(int location)
    {
         //どのアイテムを出すのかをランダムに設定
        int num = Random.Range(0, 10);
        if (num <= 1)
        {
            //コーンをx軸方向に一直線に生成
            for (float j = -1; j <= 1; j += 0.4f)
            {
                GameObject cone = Instantiate(conePrefab) as GameObject;
                cone.transform.position = new Vector3(4 * j, cone.transform.position.y, location);
            }
        }
        else
        {
            //レーンごとにアイテムを生成
            for (int j = -1; j < 2; j++)
            {
                //アイテムの種類を決める
                int item = Random.Range(1, 11);
                //アイテムを置くZ座標のオフセットをランダムに設定
                int offsetZ = Random.Range(-5, 6);
                //60%コイン配置:30%車配置:10%何もなし
                if (1 <= item && item <= 6)
                {
                    //コインを生成
                    GameObject coin = Instantiate(coinPrefab) as GameObject;
                    coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, location + offsetZ);
                }
                else if (7 <= item && item <= 9)
                {
                    //車を生成
                    GameObject car = Instantiate(carPrefab) as GameObject;
                    car.transform.position = new Vector3(posRange * j, car.transform.position.y, location + offsetZ);
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        //定期的にアイテムを生成する。まず距離を測る
        int distance = Mathf.RoundToInt(this.unityChan.transform.position.z) - startPos;

        //15m距離になったらまたアイテムを生成
        if (distance >= 15 && startPos + distance < goalPos - 60 && UCC.isEnd == false)
        {
            startPos += distance;
            int location = startPos + 45;
            CreateItems(location);
        }
	}
}
