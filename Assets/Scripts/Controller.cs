using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;
using tools;
using UnityNetwork;
using System.IO;
using TMPro;

public class Controller : MonoBehaviour
{
    public enum MessageID
    {
        Chat = 100,  // ��ʾ����Ψһ����Ϣ��־��
    }

    ChatHandler eventHandler;

    public TMP_Text revTxt;  // UI�ؼ� �յ���������Ϣ
    public TMP_InputField inputText; // UI�ؼ� �����������Ϣ
    public Button sendMsgButton; // UI�ؼ� ����������Ϣ��ť
    public Image victoryImage;//ʤ��ͼƬ
    public Image defeatImage;//ʧ��ͼƬ
    public Image retractImage;//�������ͼƬ
    public Image ppeaceImage;//�������ͼƬ
    public Image peaceImage;//����ͼƬ
    public Button prepareAgain;//����һ�� ׼�� ����Ҫ��������������Ϣ��֪ͨ�Է��Լ���׼��
    public Button backHome;//���أ���������  ����Ҫ��ͼƬ�Ͱ�ť��ʧ��֪ͨ�Է��Լ����뿪
    public Button retractMoveButton;//���尴ť
    public Button acceptRetract;//���ܻ��尴ť
    public Button refuseRetract;//�ܾ����尴ť
    public Button refusePeace;//�ܾ���Ͱ�ť
    public Button acceptPeace;//������Ͱ�ť
    public Button peaceButton;//������Ͱ�ť
    public Button surrenderButton; //Ͷ����ť


    //��Ƶ
    public AudioSource downButton;
    public AudioSource luozi;
    public AudioSource shengli;
    public AudioSource shibai;
    public AudioSource xuanzi;
    public AudioSource jiangjun;
    public AudioSource heqi;
    public AudioSource chizi;

    private GameObject Image_victory;//���UI�ؼ���GameObject
    private GameObject Image_defeat;
    private GameObject Prepare;
    private GameObject Back;
    private GameObject Acceptetract;
    private GameObject Refuse_retract;
    private GameObject Image_restracy;
    private GameObject Image_peace;
    private GameObject Image_ppeace;
    private GameObject Refuse_peace;
    private GameObject Accept_peace;

    public StringBuilder[,] backupChessBoard = new StringBuilder[11, 11];
    public StringBuilder[,] Init = new StringBuilder[11, 11];//���������ÿ��λ�����ӵ�����
    public StringBuilder[,] chessbord = new StringBuilder[11, 11];//���������ÿ��λ�����ӵ�����(��ɫ��
    public Dictionary<KeyValuePair<int, int>, KeyValuePair<float, float>> Map = new();
    public KeyValuePair<int, int>[] backupFlag = new KeyValuePair<int, int>[4];
    private float closex = (float)-3.95;
    private float closey = (float)-4.15;
    private float closex_plus = (float)-1.48;
    private float step_long = (float)0.8;
    private bool test;
    private bool canRetract = false;
    private string camp = new("");
    private string connectnum = new("");
    private bool isYourTurn = false;
    private int cnt = 0;
    private KeyValuePair<int, int> last_hit;
    private KeyValuePair<int, int> user_jiang = new(1,5);
    private KeyValuePair<int, int> enermy_jiang = new(10,5);
    private GameObject enermy_last;
    private GameObject enermy_now ;
    private GameObject player_last;
    private GameObject player_now;
    // Start is called before the first frame update
    void Start()
    {
        Image_ppeace = GameObject.Find("Image_ppeace");
        Image_peace = GameObject.Find("Image_peace");
        Refuse_peace = GameObject.Find("Refusepeace");
        Accept_peace = GameObject.Find("Accetpeace");
        Acceptetract = GameObject.Find("Acceptetract");
        Refuse_retract = GameObject.Find("Refuseretract");
        Image_restracy = GameObject.Find("Image_retract");
        Image_victory = GameObject.Find("Image_victory");
        Image_defeat = GameObject.Find("Image_defeat");
        Prepare = GameObject.Find("Prepare");
        Back = GameObject.Find("Back");
        Image_ppeace.SetActive(false);
        Image_peace.SetActive(false);
        Refuse_peace.SetActive(false);
        Accept_peace.SetActive(false);
        Acceptetract.SetActive(false);
        Refuse_retract.SetActive(false);
        Image_restracy.SetActive(false);
        Image_victory.SetActive(false);
        Image_defeat.SetActive(false);
        Prepare.SetActive(false);
        Back.SetActive(false);
        enermy_last = GameObject.Find("enermy_last");
        enermy_now  = GameObject.Find("enermy_now");
        player_last  = GameObject.Find("player_last");
        player_now = GameObject.Find("player_now");
        // �����ﴴ��ChatManagerʵ��
        eventHandler = new ChatHandler();
        // ע��һ��������Ϣ
        eventHandler.AddHandler((short)TCPPeer.MessageID.OnConnected, OnConnected);
        eventHandler.AddHandler((short)TCPPeer.MessageID.OnConnectFail, OnConnectFailed);
        eventHandler.AddHandler((short)TCPPeer.MessageID.OnDisconnect, OnLost);
        eventHandler.AddHandler((short)MessageID.Chat, OnChat);

        eventHandler.ConnectToServer();

        sendMsgButton.onClick.AddListener(delegate ()
        {
            SendChat(0,0,0,0);  // �����ť������Ϣ
            downButton.Play();
        });
        prepareAgain.onClick.AddListener(delegate ()
        {
            downButton.Play();
            if (camp.Equals("r"))
            {
                SendChat(0, 0, -1, 0);
            }
            else
            {
                SendChat(0, 0, -2, 0);
            }
            nextAction();
        });
        backHome.onClick.AddListener(delegate ()
        {
            downButton.Play();
            revTxt.text = "�����������ս��";
            nextAction();
        });
        retractMoveButton.onClick.AddListener(delegate ()
        {
            downButton.Play();
            if (!canRetract)
            {
                revTxt.text = "������������";
            }
            else if (isYourTurn)
            {
                revTxt.text = "�����ڶԷ����ж��غϻ���";
            }
            else
            {
                revTxt.text = "�ȴ��Է�ͬ�����";
                SendChat(0, 0, -3, 0);//���͸��Է���Ϣ
            }
        });
        acceptRetract.onClick.AddListener(delegate ()
        {
            downButton.Play();
            isYourTurn = false;
            canRetract = false;
            retractChessMove();
            Acceptetract.SetActive(false);
            Refuse_retract.SetActive(false);
            Image_restracy.SetActive(false);
            SendChat(0, 0, -4, 0);
        });
        refuseRetract.onClick.AddListener(delegate ()
        {
            downButton.Play();
            Acceptetract.SetActive(false);
            Refuse_retract.SetActive(false);
            Image_restracy.SetActive(false);
            SendChat(0, 0, -5, 0);
        });
        surrenderButton.onClick.AddListener(delegate ()
        {
            downButton.Play();
            Defeat();
            SendChat(0, 0, -7, 0);
        });
        peaceButton.onClick.AddListener(delegate ()
        {
            downButton.Play();
            SendChat(0, 0, -8, 0);
        });
        acceptPeace.onClick.AddListener(delegate ()
        {
            downButton.Play();
            heqi.Play();
        //    revTxt.text = "�Է�ͬ�����";
            Image_ppeace.SetActive(false);
            Image_peace.SetActive(true);
            Prepare.SetActive(true);
            Back.SetActive(true);
            Refuse_peace.SetActive(false);
            Accept_peace.SetActive(false);
            SendChat(0, 0, -9, 0);
        });
        refusePeace.onClick.AddListener(delegate ()
        {
            Refuse_peace.SetActive(false);
            Accept_peace.SetActive(false);
            Image_ppeace.SetActive(false);
            downButton.Play();
            SendChat(0, 0, -10, 0);
        });
        for (int i = 1; i <= 10; i++)
        {
            for(int j = 1; j <= 9; j++)
            {
                Init[i, j] = new StringBuilder("");
            }
        }
        Init[1, 1].Append("b_che1");
        Init[1, 2].Append("b_ma1");
        Init[1, 3].Append("b_xiang1");
        Init[1, 4].Append("b_shi1");
        Init[1, 5].Append("b_jiang");
        Init[1, 6].Append("b_shi2");
        Init[1, 7].Append("b_xiang2");
        Init[1, 8].Append("b_ma2");
        Init[1, 9].Append("b_che2");
        Init[3, 2].Append("b_pao1");
        Init[3, 8].Append("b_pao2");
        Init[4, 1].Append("b_zu1");
        Init[4, 3].Append("b_zu2");
        Init[4, 5].Append("b_zu3");
        Init[4, 7].Append("b_zu4");
        Init[4, 9].Append("b_zu5");
        Init[10, 1].Append("r_che1");
        Init[10, 2].Append("r_ma1");
        Init[10, 3].Append("r_xiang1");
        Init[10, 4].Append("r_shi1");
        Init[10, 5].Append("r_jiang");
        Init[10, 6].Append("r_shi2");
        Init[10, 7].Append("r_xiang2");
        Init[10, 8].Append("r_ma2");
        Init[10, 9].Append("r_che2");
        Init[8, 2].Append("r_pao1");
        Init[8, 8].Append("r_pao2");
        Init[7, 1].Append("r_bing1");
        Init[7, 3].Append("r_bing2");
        Init[7, 5].Append("r_bing3");
        Init[7, 7].Append("r_bing4");
        Init[7, 9].Append("r_bing5");
        for (int i = 1; i <= 5; i++)
        {
            for (int j = 1; j <= 9; j++)
            {
                KeyValuePair<int, int> temp1 = new(i, j);
                KeyValuePair<float, float> temp2 = new(closex + step_long * i, closey + step_long * j);
                Map.Add(temp1, temp2);
            }
        }
        for (int i = 6; i <= 10; i++)
        {
            for (int j = 1; j <= 9; j++)
            {
                KeyValuePair<int, int> temp1 = new(i, j);
                KeyValuePair<float, float> temp2 = new(closex_plus + step_long * (i - 3), closey + step_long * j);
                Map.Add(temp1, temp2);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        eventHandler.ProcessPackets();
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 truelocation;
            truelocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            truelocation.z = (float)0;
            //Debug.Log(truelocation);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                KeyValuePair<int, int> rec = GetLocation(truelocation);
                if (chessbord[rec.Key, rec.Value].Length != 0&&cnt==0&&isYourTurn)
                {
                    if(chessbord[rec.Key, rec.Value].ToString().Substring(0, 1).Equals(camp))//ֻ��ѡ���Լ�������
                    {
                        show(rec,player_last,false);

                        show(rec,player_now,true);
                        cnt = 1;
                        last_hit = rec;
                        xuanzi.Play();
                    }
                }
                else if(cnt == 1)//&& chessbord[rec.Key, rec.Value].Length == 0
                {
                    
                    string camp1 = chessbord[last_hit.Key, last_hit.Value].ToString().Substring(0, 1);//����ϴε�����ӵĺ������
                   //�����жϴ˴��ƶ��Ƿ��ǲٿ���
                    test = chessbord[last_hit.Key, last_hit.Value].ToString().Substring(0, 1).Equals(camp) ? true : false;
                    ArrayList moves = ChessRules.Can_Go(last_hit, chessbord[last_hit.Key, last_hit.Value].ToString(),chessbord,test);//���ѡ���������п��ƶ�λ��
                    //Debug.Log("moves : "+moves.Count);
                    if (moves.Contains(rec))
                    {
                        canRetract = true;
                        revTxt.text = "�Է��غ�";
                        show(last_hit, player_last,true);
                        show(rec, player_now,true);
                        SendChat(last_hit.Key,last_hit.Value,rec.Key,rec.Value);
                        if (chessbord[rec.Key, rec.Value].Length!=0)
                        {
                            if (chessbord[rec.Key, rec.Value].ToString().Substring(2, 2).Equals("ji"))//�ƶ����յ��ǽ�����ʤ��
                            {
                                Move(last_hit, rec, camp1);
                                Vitory();
                            }
                            else
                            {
                                Move(last_hit, rec, camp1);

                                if (Check.IsOver(false, camp.Equals("r") ? "b" : "r", enermy_jiang, chessbord))
                                {
                                    //Debug.Log("Ӯ��");
                                    Vitory();
                                }
                                else if (Check.IsAttackedGeneral(false, camp.Equals("r") ? "b" : "r", enermy_jiang, chessbord))
                                {
                                    jiangjun.Play();
                                }
                            }
                            
                        }
                        else if (chessbord[last_hit.Key, last_hit.Value].ToString().Substring(2, 2).Equals("ji"))
                        {
                            user_jiang = rec;
                            Move(last_hit, rec, camp1);//����ƶ�Ԥ��������ƶ������ڣ���Ҫ��Ӷ����ͽ����ж�
                            if (Check.IsOver(false, camp.Equals("r") ? "b" : "r", enermy_jiang, chessbord))
                            {
                                //Debug.Log("Ӯ��");
                                Vitory();
                            }
                            else if (Check.IsAttackedGeneral(false, camp.Equals("r") ? "b" : "r", enermy_jiang, chessbord))
                            {
                                jiangjun.Play();
                            }
                        }
                        else
                        {
                            Move(last_hit, rec, camp1);//����ƶ�Ԥ��������ƶ������ڣ���Ҫ��Ӷ����ͽ����ж�
                            if (Check.IsOver(false, camp.Equals("r") ? "b" : "r", enermy_jiang, chessbord))
                            {
                                //Debug.Log("Ӯ��");
                                Vitory();
                            }
                            else if (Check.IsAttackedGeneral(false, camp.Equals("r") ? "b" : "r", enermy_jiang, chessbord))
                            {
                                jiangjun.Play();
                            }
                        }
                    }
                    else if(chessbord[rec.Key, rec.Value].Length == 0)
                    {
                        show(rec, player_last, false);
                        show(rec, player_now, false);
                        cnt = 0;
                    }
                    else if(cnt == 1 && chessbord[rec.Key, rec.Value].Length != 0)
                    {
                        xuanzi.Play();
                        show(rec, player_now, true);
                        last_hit = rec;
                    }
                }

            }
        }
    }
    public void Initialize(string x)
    {
        enermy_last.SetActive(false);
        enermy_now.SetActive(false);
        player_last.SetActive(false);
        player_now.SetActive(false);
        for(int i = 0; i <= 3; i++)
        {
            backupFlag[i] = new(0, 0);
        }
        for (int i = 1; i <= 10; i++)
        {
            for (int j = 1; j <= 9; j++)
            {
                chessbord[i, j] = new StringBuilder("");
                backupChessBoard[i,j] = new StringBuilder("");
            }
        }
        if (x.Equals("r"))
        {
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 9; j++)
                {
                    chessbord[10 - i + 1, 9 - j + 1].Clear();
                    if (Init[i, j].Length != 0)
                    {
                        isYourTurn = true;
                        chessbord[10 - i + 1, 9 - j + 1].Append(Init[i, j]);
                        GameObject gameObject = GameObject.Find(chessbord[10 - i + 1, 9 - j + 1].ToString());
                        KeyValuePair<int, int> temp1 = new(10 - i + 1, 9 - j + 1);
                        //Debug.Log(chessbord[10 - i + 1, 9 - j + 1].ToString());
                        gameObject.SetActive(true);
                        Vector3 vector = new(Map[temp1].Value, Map[temp1].Key, 0);
                        gameObject.transform.position = vector;
                    }
                }
            }
        }
        else if (x.Equals("b"))
        {
            //Debug.Log("��ʼ��Ϊ��");
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 9; j++)
                {
                    //   Debug.Log(chessbord[i, j].Length);
                    if (Init[i, j].Length != 0)
                    {
                        chessbord[i, j].Append(Init[i, j]);
                        GameObject gameObject = GameObject.Find(chessbord[i, j].ToString());
                        KeyValuePair<int, int> temp1 = new(i, j);
                        //Debug.Log(chessbord[i, j].ToString());
                        gameObject.SetActive(true);
                        Vector3 vector = new(Map[temp1].Value, Map[temp1].Key, 0);
                        gameObject.transform.position = vector;
                    }
                }
            }
        }
    }

    public void retractChessMove()
    {
        isYourTurn = true;
        for (int i = 1; i <= 10; i++)
        {
            for (int j = 1; j <= 9; j++)
            {
                chessbord[i, j].Clear();
                if (backupChessBoard[i,j].Length != 0)
                {
                    chessbord[i, j].Append(backupChessBoard[i, j]);
                    //Debug.Log(chessbord[i, j].ToString());
                    GameObject gameObject = GameObject.Find(chessbord[i, j].ToString());
                    KeyValuePair<int, int> temp1 = new(i, j);
                    gameObject.SetActive(true);
                    Vector3 vector = new(Map[temp1].Value, Map[temp1].Key, 0);
                    gameObject.transform.position = vector;
                }
            }
        }
    }
    public void show(KeyValuePair<int, int> location, GameObject box, bool state)
    {
        box.SetActive(state);
        Vector3 vector = new(Map[location].Value, Map[location].Key, 0);
        box.transform.position = vector;
    }
    public void Move(KeyValuePair<int, int> last_hit,KeyValuePair<int, int> rec,string camp1)
    {
        for(int i = 1; i <= 10; i++)
        {
            for(int j = 1;j <= 9; j++)
            {
                backupChessBoard[i, j].Clear();
                if(chessbord[i,j].Length != 0)
                {
                    backupChessBoard[i, j].Append(chessbord[i, j]);
                    //Debug.Log(backupChessBoard[i, j] + "|  |" + backupChessBoard[i, j].ToString());
                }
            }
        }
        GameObject gameObject = GameObject.Find(chessbord[last_hit.Key, last_hit.Value].ToString());//�ҵ��ϴε����λ��
        Vector3 vector = new(Map[rec].Value, Map[rec].Key, 0); //��ȡ��ε���ľ���λ����Ϣ
        if (chessbord[rec.Key, rec.Value].Length == 0)
        {
            gameObject.transform.position = vector;
            chessbord[rec.Key, rec.Value].Append(chessbord[last_hit.Key, last_hit.Value]);
            chessbord[last_hit.Key, last_hit.Value].Clear();
            //Debug.Log(chessbord[last_hit.Key, last_hit.Value].Length);
            //Debug.Log(chessbord[rec.Key, rec.Value].ToString());
            cnt = 0;
        }
        else if (!chessbord[rec.Key, rec.Value].ToString().Substring(0, 1).Equals(camp1))
        {
            GameObject gameObject1 = GameObject.Find(chessbord[rec.Key, rec.Value].ToString());
            Vector3 faraway = new(1000,1000,0);
            gameObject1.transform.position = faraway;
            gameObject.transform.position = vector;
            chessbord[rec.Key, rec.Value].Clear();
            chessbord[rec.Key, rec.Value].Append(chessbord[last_hit.Key, last_hit.Value]);
            chessbord[last_hit.Key, last_hit.Value].Clear();
            //Debug.Log(chessbord[last_hit.Key, last_hit.Value].Length);
            //Debug.Log(chessbord[rec.Key, rec.Value].ToString());
            chizi.Play();
            cnt = 0;
        }
        luozi.Play();
    }

    private KeyValuePair<int, int> GetLocation(Vector3 location)
    {
        //Debug.Log("GetMouseButtonDown");
     //   GameObject gameObject = GameObject.Find("b_jiang");
        int x = (int)System.Math.Floor((location.x + (float)4.0) / step_long), y = (int)System.Math.Floor((location.y + (float)4.1) / step_long);
        float min = (float)100.10;
        KeyValuePair<int, int> rec = new();
        /*Debug.Log(x);
        Debug.Log(y);*/
        for (int i = y; i <= 10 && i <= (y + 1); i++)
        {
            for (int j = x; j <= 9 && j <= (x + 1); j++)
            {
                KeyValuePair<int, int> temp1 = new(i, j);
                if (Map.ContainsKey(temp1))
                {
                    float temp = (Map[temp1].Value - location.x) * (Map[temp1].Value - location.x) + (Map[temp1].Key - location.y) * (Map[temp1].Key - location.y);
                    if (temp < min)
                    {
                        rec = temp1;
                        min = temp;
                    }
                }
            }
        }
        //Debug.Log(rec);
        return rec;
    }
    // ����ͻ���ȡ���������������
    public void OnConnected(Packet packet)
    {
        //Debug.Log("�ɹ����ӵ�������");
    }

    // ����ͻ��������������ʧ��
    public void OnConnectFailed(Packet packet)
    {
        revTxt.text = "���ӷ�����ʧ�ܣ����˳�";
        //Debug.Log("���ӷ�����ʧ�ܣ����˳�");
    }

    // ����ʧ����
    public void OnLost(Packet packet)
    {
        revTxt.text = "��ʧ������������ӣ����˳�����������";
        //Debug.Log("��ʧ�������������");
    }

    public void nextAction()
    {
        Image_peace.SetActive(false);
        Image_victory.SetActive(false);
        Image_defeat.SetActive(false);
        Prepare.SetActive(false);
        Back.SetActive(false);
    }
    public void Vitory()
    {
        shengli.Play();
        Image_victory.SetActive(true);
        Prepare.SetActive(true);
        Back.SetActive(true);
    }
    public void Defeat()
    {
        shibai.Play();
        Image_defeat.SetActive(true);
        Prepare.SetActive(true);
        Back.SetActive(true);
    }

    // ����������Ϣ
    void SendChat(int oringinX,int oringinY,int endX,int endY)
    {
        isYourTurn = false;
     //   canRetract = true;
        // ����Э��
        Chat.ChatProto proto = new Chat.ChatProto();
        proto.connectNum = inputText.text;
        proto.originX = oringinX;
        proto.originY = oringinY;
        proto.endX = endX;
        proto.endY = endY;
        proto.fightState="";
        if (proto.connectNum.Length != 0)
        {
            connectnum = proto.connectNum;
        }
        else
        {
            proto.connectNum = connectnum;
        }
        // ���л�
        byte[] bs = Packet.Serialize<Chat.ChatProto>(proto);

        // �������ݰ�
        Packet p = new Packet((short)MessageID.Chat);
        using (MemoryStream stream = p.Stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(bs.Length);
            writer.Write(bs);
            p.EncodeHeader(stream);
        }
        // ���͵�������
        eventHandler.SendMessage(p);

        //��������
        inputText.text = "";
    }

    // ����������Ϣ
    public void OnChat(Packet packet)
    {
        
        byte[] buffer = null;
        using (MemoryStream stream = packet.Stream)
        {
            BinaryReader reader = new BinaryReader(stream);
            int len = reader.ReadInt32();
            buffer = reader.ReadBytes(len);
        }
        Chat.ChatProto proto = Packet.Deserialize<Chat.ChatProto>(buffer);
    //    revTxt.text = proto.fightState+proto.connectNum + ":" + proto.originX + proto.originY + proto.endX + proto.endY;  // ��ʾ�յ�����Ϣ
        //Debug.Log("�յ�����������Ϣ:" + proto.originX + proto.originY + proto.endX + proto.endY);
        KeyValuePair<int, int> last = new(10-proto.originX+1, 9-proto.originY+1);
        KeyValuePair<int, int> rec = new(10-proto.endX+1, 9-proto.endY+1);
      //  Debug.Log("")
        if (proto.originX != 0 && proto.originY != 0 && proto.endX != 0 && proto.endY != 0) {
            if (chessbord[rec.Key, rec.Value].Length!=0)
            {
                if(chessbord[rec.Key, rec.Value].ToString().Substring(2, 2).Equals("ji"))
                {
                    show(last, enermy_last,true);
                    show(rec, enermy_now,true); 
                    Move(last, rec, chessbord[last.Key, last.Value].ToString().Substring(0, 1));
                    revTxt.text = "ʧ��";
                    Defeat();
                    return;
                }
            }
            else if (chessbord[last.Key, last.Value].ToString().Substring(2, 2).Equals("ji"))
            {
                enermy_jiang = rec;
            }
            Move(last, rec, chessbord[last.Key, last.Value].ToString().Substring(0, 1));
            revTxt.text = "��Ļغ�";
            canRetract = false;
            isYourTurn = true;
            bool isattacked = Check.IsAttackedGeneral(true, camp, user_jiang, chessbord);
            //Debug.Log(isattacked);
            if (isattacked)
            {
                if(Check.IsOver(true, camp, user_jiang, chessbord))
                {
                    //Debug.Log("��������������");
                    Defeat();
                }
                else
                {
                    jiangjun.Play();
                    //Debug.Log("��");
                }
            }
            show(last, enermy_last,true);
            show(rec, enermy_now,true);
        }
        else if(proto.endX == -3)
        {
      //      revTxt = ""
            Acceptetract.SetActive(true);
            Refuse_retract.SetActive(true);
            Image_restracy.SetActive(true);
        }
        else if(proto.endX == -4)
        {
            revTxt.text = "�Է�ͬ����壡";
            retractChessMove();
        }
        else if(proto.endX == -5)
        {
            revTxt.text = "�Է��ܾ����壬�������Ϸ";
        }
        else if(proto.endX == -6)
        {
            revTxt.text = proto.fightState;
        }
        else if(proto.endX == -7)
        {
            Vitory();
            shengli.Play();
        }
        else if (proto.endX == -8)
        {
            revTxt.text = "�Է��������";
            Image_ppeace.SetActive(true);
          //  Image_peace.SetActive(false);
            Refuse_peace.SetActive(true);
            Accept_peace.SetActive(true);
        }
        else if(proto.endX == -9)
        {
            heqi.Play();
            revTxt.text = "�Է�ͬ�����";
            Image_ppeace.SetActive(false);
            Image_peace.SetActive(true);
            Prepare.SetActive(true);
            Back.SetActive(true);
/*            Refuse_peace.SetActive(false);
            Accept_peace.SetActive(false);*/
        }        
        else if(proto.endX == -10)
        {
            revTxt.text = "�Է��ܾ�����,�������Ϸ";
/*            Image_ppeace.SetActive(false);
            Refuse_peace.SetActive(false);
            Accept_peace.SetActive(false);*/
        }
        else
        {
      //      revTxt.text = proto.fightState;
            if (proto.usingColor.Equals("b")|| proto.usingColor.Equals("r"))
            {
                camp = proto.usingColor;
                Initialize(proto.usingColor);
            }
            //Debug.Log("���ƶ���Ϣ,���ԣ�"+proto.connectNum+"��ʼ����Ϣ: "+proto.usingColor);
        }
    }
}