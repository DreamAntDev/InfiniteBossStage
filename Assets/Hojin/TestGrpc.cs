using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grpc.Core;
using Haru;
public class TestGrpc : MonoBehaviour
{
    public static long UUID;
    private string DOMAIN = "127.0.0.1:50051";
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("TestGrpc start");
        Channel channel = new Channel(DOMAIN, ChannelCredentials.Insecure);
        // 서버 커넥션 생성
        var client = new Haru.version1.version1Client(channel);
        // 신규 어카운트 아이디 발급
        var CreateAccountReply = client.CreateAccount(new AccountRequest {});
        Debug.Log("Create Account" + CreateAccountReply);
        UUID = CreateAccountReply.ID;
        Debug.Log("Set UUID " + UUID);
        // 프로필 닉네임 업데이트
        var UpdateProfileReply = client.UpdateProfile(new ProfileRequest { ID = UUID, Nickname = "Unity" });
        Debug.Log("Update Profile" + UpdateProfileReply);
        // 아이디로 프로필 조회
        var GetProfileReply = client.GetProfile(new ProfileRequest {ID = UUID});
        Debug.Log("Get Profile" + GetProfileReply);
        // 포인트 증가
        var IncrPointReply = client.IncrPoint(new PointRequest {ID = 1, Point = 1});
        Debug.Log("Incr Point " + IncrPointReply);
        // 포인트 조회
        var GetPointReply = client.GetPoint(new PointRequest {ID = 1});
        Debug.Log("Get Point " + GetPointReply);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
