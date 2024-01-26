using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{

    [SerializeField]
    private Text skillText;  
    [SerializeField]
    private GameObject BackGround;
    public static  bool skillFlag;
    [SerializeField]
    private Text alphaKey;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BackGround.SetActive(skillFlag);
        if(CharaSelectManager.charaSelectScreen)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                if(!skillFlag)
                {
                    alphaKey.text="１キーを押して戻る";
                    skillFlag = true;
                    switch (TeamCharacter.selectCharaNumber)
                    {
                        case 0:
                            {
                                skillText.text="スラッシュ\n敵に向かって勢いよく斬りつけ\n確実なダメージを与える技";
                            }
                            break;
                        case 1:
                            {
                                skillText.text="守護の構え\n守りの姿勢を取り\n自身に中程度のダメージカットを付与する";
                            }
                            break;
                        case 2:
                            {
                                skillText.text = "加速する未来\n水晶を用いて運命と時間に干渉し、\nコマンドの投げれる速度を上昇させる。";
                            }
                            break;
                        case 3:
                            {
                                skillText.text = "呪符・鈍足香\n呪いの込められた札を用いて、\n敵の行動速度をかなり減少させる";
                            }
                            break;
                        case 4:
                            {
                                skillText.text = "サンバレット\n炎の力を杖先に込め、敵に向かって放つ。\nレティシアの調子で火力が変化する";
                            }
                            break;
                        case 5:
                            {
                                skillText.text = "散桜\n散りゆく桜の花びらのような剣舞で斬りかかる\n成功率によって攻撃回数が変化する。";

                            }
                            break;
                        case 6:
                            {
                                skillText.text = "薬品投擲\n怪しい薬品を敵に向かって投擲する。\nダメージとともに、まれに敵を毒に侵すこがある。";
                            }
                            break;
                        case 7:
                            {
                                skillText.text = "音撃波\n楽器をけたたましく鳴らして音撃波を生成する\n敵に中程度のダメージを与える";
                            }
                            break;
                    }
                }
                else
                {
                    skillFlag=false;
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (!skillFlag)
                {
                    alphaKey.text = "２キーを押して戻る";
                    skillFlag = true;
                    switch (TeamCharacter.selectCharaNumber)
                    {
                        case 0:
                            {
                                skillText.text = "闘志入魂\n熱き心を滾らせ、味方全体の\n攻撃力をとてつもなく上昇させる。";
                            }
                            break;
                        case 1:
                            {
                                skillText.text = "挑発\n敵を過剰に挑発して苛立たせる。\n自身が狙われやすくなる。";
                            }
                            break;
                        case 2:
                            {
                                skillText.text = "減速する過去\n水晶を用いて運命と時間に干渉し、\nコマンドの流れる速度を減少させる。";
                            }
                            break;
                        case 3:
                            {
                                skillText.text = "御神水です！\n神聖な水を必要とする者へ分け与る\n一番体力の低い味方をかなり回復する";
                            }
                            break;
                        case 4:
                            {
                                skillText.text = "アイスランス\n凍てつく氷塊を頭上に生成し、敵に向けて叩き落す。\n稀にすさまじいダメージが出る";
                            }
                            break;
                        case 5:
                            {
                                skillText.text = "血刃\n刀身に念を込め血塗られた刃に変化させる。\n敵に攻撃したとき、刃が共鳴しHPを回復する。";

                            }
                            break;
                        case 6:
                            {
                                skillText.text = "アドレ注射\nいかにも怪しい薬を味方全員に注入する。\nアドレナリンが大量分泌され攻撃力が上昇する。";
                            }
                            break;
                        case 7:
                            {
                                skillText.text ="闘いの旋律\n心躍る熱いビートを奏でる。\nバフに応じて、味方全体の攻撃力を上昇させる";
                            }
                            break;
                    }
                }
                else
                {
                    skillFlag = false;
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (!skillFlag)
                {
                    alphaKey.text = "３キーを押して戻る";
                    skillFlag = true;
                    switch (TeamCharacter.selectCharaNumber)
                    {
                        case 0:
                            {
                                skillText.text = "妨害工作\n動きを封じるような罠で\n敵の行動速度を少しだけ減少させる。";
                            }
                            break;
                        case 1:
                            {
                                skillText.text = "威圧\n堂々たる騎士の圧力を放つ。\n敵の攻撃力を少し下げる。";
                            }
                            break;
                        case 2:
                            {
                                skillText.text = "ありえた選択\n水晶を用いて様々な世界戦に干渉する。\nコマンドの成功率を上昇させる。";
                            }
                            break;
                        case 3:
                            {
                                skillText.text = "護符・厄払\n悪しきものを近づけさせない札で\n味方全体に状態異常を無効化する障壁を展開する。";
                            }
                            break;
                        case 4:
                            {
                                skillText.text = "マジックバレル\n浮遊する魔方陣を周辺に多重展開する。\n見方が行動する度に追加攻撃が発生する。";
                            }
                            break;
                        case 5:
                            {
                                skillText.text = "威風堂々\n侍魂を燃やし、敵前に身を乗り出す。\n自身が狙われやすくなる\nさらに次の敵の攻撃を確率で回避する。";

                            }
                            break;
                        case 6:
                            {
                                skillText.text = "睡眠薬射出\n薬物の含まれた針を敵に投擲する。\n低確率で相手を睡眠状態にし、行動不能にする。";
                            }
                            break;
                        case 7:
                            {
                                skillText.text = "祈りのイムン\n癒し効果のある優しい旋律を奏でる。\n一定時間味方が行動する度に回復するようになる";
                            }
                            break;
                    }
                }
                else
                {
                    skillFlag = false;
                }
            }
                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    if (!skillFlag)
                    {
                        alphaKey.text = "４キーを押して戻る";
                        skillFlag = true;
                        switch (TeamCharacter.selectCharaNumber)
                        {
                            case 0:
                                {
                                    skillText.text = "応急手当\n応急的な処置を行い\n自身のHPを少し回復する。";
                                }
                                break;
                            case 1:
                                {
                                    skillText.text = "刺突\n敵に向かって駆け出し、\n力を込めた一刺しで攻撃する";
                                }
                                break;
                            case 2:
                                {
                                    skillText.text = "結末への調整\n導かれる結末から望む結末へと調整する\n相手の現存HPに合わせた攻撃力で攻撃する。";
                                }
                                break;
                            case 3:
                                {
                                    skillText.text = "霊符・風鎌\n風の力を身に纏い、\n敵の攻撃を少し反射するようになる。";
                                }
                                break;
                            case 4:
                                {
                                    skillText.text = "魔力次弾装填\n精神を集中させ魔方陣生成を加速させる。\n次に発動するマジックバレルの効果が上昇する。";
                                }
                                break;
                            case 5:
                                {
                                    skillText.text = "兜割\n敵の弱点を突く剣技で斬りかかる。\n敵が弱体化していた場合、さらに強力な一閃放つ。";

                                }
                                break;
                            case 6:
                                {
                                    skillText.text = "簡易オペ\nその場で簡易的な手術を実行する。\n一部のデバフを解除することができる。";
                                }
                                break;
                            case 7:
                                {
                                    skillText.text = "ガブ・サンク\n立ち上がる気力が湧き上がるような旋律を奏でる。味方全体のHPが少し回復し\nわずかな時間行動速度が速くなる。";
                                }
                                break;
                        }
                    }
                    else
                    {
                        skillFlag = false;
                    }
                }
            if (!skillFlag)
            {
                skillText.text="";
                alphaKey.text="";
            }
    }
}
}