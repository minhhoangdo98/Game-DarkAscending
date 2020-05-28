using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Letter : MonoBehaviour
{
    [SerializeField]
    private GameObject letterPanel, objectUnlock;
    private GameObject buttonAction;
    [SerializeField]
    private Text textLetter;
    [SerializeField]
    private bool interacable = true;
    private GameController3D gc3d;

    private void Start()
    {
        buttonAction = GameObject.FindGameObjectWithTag("ButtonAction");
        gc3d = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController3D>();
    }

    private void ActionPerform()
    {
        //Doc thu
        if (interacable)
        {
            ReadLetter();
        }
    }

    private void ChangeText(Text actionText)
    {
        switch (gc3d.languages)
        {
            case "en":
                actionText.text = "Read";
                break;
            case "vi":
                actionText.text = "Đọc";
                break;
        }
    }

    //Doc thu va dan cot truyen
    private void ReadLetter()
    {
        //Kiem tra result va ngay tuong ung
        if (gc3d.result == "win")
        {
            switch (gc3d.ngay)
            {
                case 0:
                    switch (gc3d.languages)
                    {
                        case "en":
                            textLetter.text = "Message for Test subject\n\n\n Prisoner, we will perform a test on you.\n First,\n Try to get out of this room!\n After that, there is a locked door outside, find a key and open it\n We have prepare a Gift for you!\n Go now!";
                            break;
                        case "vi":
                            textLetter.text = "Lời nhắn cho đối tượng thí nghiệm\n\n\n Tù nhân, bọn ta sẽ tiến hành một thử nghiệm trên ngươi.\n Đầu tiên,\n Hãy ra khỏi căn phòng này!\n Sau đó, Có một cánh cửa đang bị khóa ở ngoài, tìm chìa khóa và mở nó!\n Bọn ta đã chuẩn bị sẵn một món quá cho ngươi!\n Giờ thì đi đi!";
                            break;
                    }
                    letterPanel.SetActive(true);
                    objectUnlock.GetComponent<OpenDoor>().locked = false;//Sau khi doc to giay thi mo khoa cua
                    break;
                case 1:
                    switch (gc3d.languages)
                    {
                        case "en":
                            textLetter.text = "Message for prisoner\n\nCongrat! congrat! You have officially become our prisoner!\n Now you are infected by a virus from that monster. The virus will erode your body and turn you into a monster that loses consciousness.\n But don't worry, if you take part in our game, you will have the antidote, go out of this room to the show. Doing well to make the audience satisfied then come here I will give you the antidote. \nAnd one more thing, don't even think about escaping, we can control the virus in you, remember that!\nNow then, let's start the show!";
                            break;
                        case "vi":
                            textLetter.text = "Lời nhắn cho tù nhân\n\n Chúc mừng! chúc mừng! Ngươi đã chính thức trở thành tù nhân của bọn ta!\n hiện giờ ngươi đang bị nhiễm một con virus từ con quái vật đó. Virus sẽ ăn mòn cơ thể ngươi và biến ngươi trở thành quái vật mất đi ý thức.\n Nhưng đừng lo, nếu ngươi tham gia vào trò chơi này của bọn ta thì ngươi sẽ có thuốc giải, hãy đi ra ngoài căn phòng này để đến buổi diễn. Nhớ thể hiện thật tốt để làm cho khán giả hài lòng sau đó về đây ta sẽ cho ngươi thuốc giải.\n Và còn một điều nữa, đừng bao giờ nghĩ đến việc trốn thoát, bọn ta có thể điều khiển virus trong người ngươi, hãy nhớ lấy điều đó!\n Giờ thì, bắt đầu buổi diễn nào!";
                            break;
                    }
                    letterPanel.SetActive(true);
                    objectUnlock.GetComponent<DoorTo2D>().locked = false;
                    break;
                case 2:
                    switch (gc3d.languages)
                    {
                        case "en":
                            textLetter.text = "\n\nCongratulations!\n The audience was satisfied, and as promised, you had the antidote!\n How do you feel? While you were unconscious, we injected an antidote into you.\n But don't be happy because you still have to work for us.\n Remember we always infect you with a new virus after removing the old virus. So if you don't work, you'll die!\n Now go out and start the next show!";
                            break;
                        case "vi":
                            textLetter.text = "\n\nChúc mừng!\n Khán giả đã rất hài lòng, và đúng như lời hứa, ngươi đã có thuốc giải!\n Cảm thấy thế nào? trong lúc ngươi bất tỉnh, bọn ta đã tiêm thuốc giải vào người ngươi.\n Nhưng đừng mừng vội bởi vì ngươi vẫn phải làm việc cho bọn ta.\n Hãy nhớ bọn ta luôn luôn tiêm nhiễm cho ngươi một con virus mới sau khi giải virus cũ. Vì vậy, nếu ngươi không làm việc thì sẽ chết! \nGiờ thì ra ngoài và bắt đầu buổi diễn tiếp theo đi!";
                            break;
                    }
                    letterPanel.SetActive(true);
                    objectUnlock.GetComponent<DoorTo2D>().locked = false;
                    break;
                default:
                    switch (gc3d.languages)
                    {
                        case "en":
                            textLetter.text = "\n\n\nNothing here! Go to the show now!";
                            break;
                        case "vi":
                            textLetter.text = "\n\n\nKhông có gì ở đây! Đi đến buổi diễn đi";
                            break;
                    }
                    letterPanel.SetActive(true);
                    objectUnlock.GetComponent<DoorTo2D>().locked = false;
                    break;
            }

        }
        else
        {
            //Gameover
            switch (gc3d.languages)
            {
                case "en":
                    textLetter.text = "\n\n\n Too bad! There will be no antidote for you! \n\nDie!";
                    break;
                case "vi":
                    textLetter.text = "\n\n\n Quá tệ! Sẽ không có thuốc giải cho ngươi đâu! \n\nChịu chết đi!";
                    break;
            }
            letterPanel.SetActive(true);
            StartCoroutine(gc3d.ToGameover());
        }           
    }

}
