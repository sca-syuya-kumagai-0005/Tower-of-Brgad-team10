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
                    alphaKey.text="�P�L�[�������Ė߂�";
                    skillFlag = true;
                    switch (TeamCharacter.selectCharaNumber)
                    {
                        case 0:
                            {
                                skillText.text="�X���b�V��\n�G�Ɍ������Đ����悭�a���\n�m���ȃ_���[�W��^����Z";
                            }
                            break;
                        case 1:
                            {
                                skillText.text="���̍\��\n���̎p�������\n���g�ɒ����x�̃_���[�W�J�b�g��t�^����";
                            }
                            break;
                        case 2:
                            {
                                skillText.text = "�������関��\n������p���ĉ^���Ǝ��ԂɊ����A\n�R�}���h�̓�����鑬�x���㏸������B";
                            }
                            break;
                        case 3:
                            {
                                skillText.text = "�����E�ݑ���\n�􂢂̍��߂�ꂽ�D��p���āA\n�G�̍s�����x�����Ȃ茸��������";
                            }
                            break;
                        case 4:
                            {
                                skillText.text = "�T���o���b�g\n���̗͂����ɍ��߁A�G�Ɍ������ĕ��B\n���e�B�V�A�̒��q�ŉΗ͂��ω�����";
                            }
                            break;
                        case 5:
                            {
                                skillText.text = "�U��\n�U��䂭���̉Ԃт�̂悤�Ȍ����Ŏa�肩����\n�������ɂ���čU���񐔂��ω�����B";

                            }
                            break;
                        case 6:
                            {
                                skillText.text = "��i����\n��������i��G�Ɍ������ē�������B\n�_���[�W�ƂƂ��ɁA�܂�ɓG��łɐN����������B";
                            }
                            break;
                        case 7:
                            {
                                skillText.text = "�����g\n�y����������܂����炵�ĉ����g�𐶐�����\n�G�ɒ����x�̃_���[�W��^����";
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
                    alphaKey.text = "�Q�L�[�������Ė߂�";
                    skillFlag = true;
                    switch (TeamCharacter.selectCharaNumber)
                    {
                        case 0:
                            {
                                skillText.text = "���u����\n�M���S����点�A�����S�̂�\n�U���͂��ƂĂ��Ȃ��㏸������B";
                            }
                            break;
                        case 1:
                            {
                                skillText.text = "����\n�G���ߏ�ɒ������ĉ՗�������B\n���g���_���₷���Ȃ�B";
                            }
                            break;
                        case 2:
                            {
                                skillText.text = "��������ߋ�\n������p���ĉ^���Ǝ��ԂɊ����A\n�R�}���h�̗���鑬�x������������B";
                            }
                            break;
                        case 3:
                            {
                                skillText.text = "��_���ł��I\n�_���Ȑ���K�v�Ƃ���҂֕����^��\n��ԑ̗͂̒Ⴂ���������Ȃ�񕜂���";
                            }
                            break;
                        case 4:
                            {
                                skillText.text = "�A�C�X�����X\n���Ă��X��𓪏�ɐ������A�G�Ɍ����Ē@�������B\n�H�ɂ����܂����_���[�W���o��";
                            }
                            break;
                        case 5:
                            {
                                skillText.text = "���n\n���g�ɔO�����ߌ��h��ꂽ�n�ɕω�������B\n�G�ɍU�������Ƃ��A�n������HP���񕜂���B";

                            }
                            break;
                        case 6:
                            {
                                skillText.text = "�A�h������\n�����ɂ���������𖡕��S���ɒ�������B\n�A�h���i��������ʕ��傳��U���͂��㏸����B";
                            }
                            break;
                        case 7:
                            {
                                skillText.text ="�����̐���\n�S���M���r�[�g��t�ł�B\n�o�t�ɉ����āA�����S�̂̍U���͂��㏸������";
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
                    alphaKey.text = "�R�L�[�������Ė߂�";
                    skillFlag = true;
                    switch (TeamCharacter.selectCharaNumber)
                    {
                        case 0:
                            {
                                skillText.text = "�W�Q�H��\n�����𕕂���悤��㩂�\n�G�̍s�����x��������������������B";
                            }
                            break;
                        case 1:
                            {
                                skillText.text = "�Ј�\n���X����R�m�̈��͂���B\n�G�̍U���͂�����������B";
                            }
                            break;
                        case 2:
                            {
                                skillText.text = "���肦���I��\n������p���ėl�X�Ȑ��E��Ɋ�����B\n�R�}���h�̐��������㏸������B";
                            }
                            break;
                        case 3:
                            {
                                skillText.text = "�아�E�\n���������̂��߂Â������Ȃ��D��\n�����S�̂ɏ�Ԉُ�𖳌��������ǂ�W�J����B";
                            }
                            break;
                        case 4:
                            {
                                skillText.text = "�}�W�b�N�o����\n���V���閂���w�����ӂɑ��d�W�J����B\n�������s������x�ɒǉ��U������������B";
                            }
                            break;
                        case 5:
                            {
                                skillText.text = "�Е����X\n������R�₵�A�G�O�ɐg�����o���B\n���g���_���₷���Ȃ�\n����Ɏ��̓G�̍U�����m���ŉ������B";

                            }
                            break;
                        case 6:
                            {
                                skillText.text = "������ˏo\n�򕨂̊܂܂ꂽ�j��G�ɓ�������B\n��m���ő���𐇖���Ԃɂ��A�s���s�\�ɂ���B";
                            }
                            break;
                        case 7:
                            {
                                skillText.text = "�F��̃C����\n�������ʂ̂���D����������t�ł�B\n��莞�Ԗ������s������x�ɉ񕜂���悤�ɂȂ�";
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
                        alphaKey.text = "�S�L�[�������Ė߂�";
                        skillFlag = true;
                        switch (TeamCharacter.selectCharaNumber)
                        {
                            case 0:
                                {
                                    skillText.text = "���}�蓖\n���}�I�ȏ��u���s��\n���g��HP�������񕜂���B";
                                }
                                break;
                            case 1:
                                {
                                    skillText.text = "�h��\n�G�Ɍ������ċ삯�o���A\n�͂����߂���h���ōU������";
                                }
                                break;
                            case 2:
                                {
                                    skillText.text = "�����ւ̒���\n������錋������]�ތ����ւƒ�������\n����̌���HP�ɍ��킹���U���͂ōU������B";
                                }
                                break;
                            case 3:
                                {
                                    skillText.text = "�아�E����\n���̗͂�g�ɓZ���A\n�G�̍U�����������˂���悤�ɂȂ�B";
                                }
                                break;
                            case 4:
                                {
                                    skillText.text = "���͎��e���U\n���_���W�����������w����������������B\n���ɔ�������}�W�b�N�o�����̌��ʂ��㏸����B";
                                }
                                break;
                            case 5:
                                {
                                    skillText.text = "����\n�G�̎�_��˂����Z�Ŏa�肩����B\n�G����̉����Ă����ꍇ�A����ɋ��͂Ȉ�M���B";

                                }
                                break;
                            case 6:
                                {
                                    skillText.text = "�ȈՃI�y\n���̏�ŊȈՓI�Ȏ�p�����s����B\n�ꕔ�̃f�o�t���������邱�Ƃ��ł���B";
                                }
                                break;
                            case 7:
                                {
                                    skillText.text = "�K�u�E�T���N\n�����オ��C�͂��N���オ��悤�Ȑ�����t�ł�B�����S�̂�HP�������񕜂�\n�킸���Ȏ��ԍs�����x�������Ȃ�B";
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