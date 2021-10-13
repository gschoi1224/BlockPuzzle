using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;    //파일의 입출력을 위한 네임스페이스
using System.Runtime.Serialization.Formatters.Binary;   //바이너리 파일 포맷을 위한 네임스페이스
using DataInfo; //데이터 저장 클래스에 접근하기 위한 네임스페이스

public class DataManager : MonoBehaviour {

    private string dataPath;

    public void Initialize()
    {
        dataPath = Application.persistentDataPath + "/gameData.dat";
    }
	
    // 데이터 저장 및 파일을 생성하는 함수
    public void Save(GameData gameData)
    {
        //바이너리 파일 포맷을 위한 Binary Formatter 생성
        BinaryFormatter bf = new BinaryFormatter();
        //데이터 저장을 위한 파일 생성
        FileStream file = File.Create(dataPath);

        //파일에 저장할 클래스에 데이터 할당
        GameData data = new GameData();
      //  data.level = gameData.level;
       // data.next = gameData.next;
     //   data.nextnext = gameData.nextnext;
     //   data.now = gameData.now;
       // data.score = gameData.score;
        //data.hold = gameData.hold;
        //data.saveMap = gameData.saveMap;
        data.optionData = gameData.optionData;

        //BinaryFormatter를 사용해 파일에 데이터 기록
        bf.Serialize(file, data);
        file.Close();
    }
    //파일에서 데이터를 추출하는 함수
    public GameData Load()
    {
        if(File.Exists(dataPath))
        {
            //파일이 존재할 경우 데이터 불러오기
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(dataPath, FileMode.Open);
            //GameData 클래스에 파일로부터 읽은 데이터를 기록
            GameData data = (GameData)bf.Deserialize(file);
            file.Close();

            return data;
        }
        else
        {
            //파일이 없을 경우 기본값을 반환
            GameData data = new GameData();

            return data;
        }
    }
}
