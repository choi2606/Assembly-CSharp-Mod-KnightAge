using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Xml.Linq;
using UnityEngine;

public class AutoManager
{
    public AutoManager()
    {
        //accountId = array5[5];
        /*configManager = new ConfigManager();
		configManager.Load(configFilePath);
		config = configManager.GetConfig(accountId) ?? new AccountConfig();*/
    }

    public static AutoManager gI()
    {
        if (instance == null)
        {
            instance = new AutoManager();
        }
        return instance;
    }

    public void stopThread()
    {
        if (thread != null)
        {
            threadStop = true;
            thread.Join();
        }
    }

    public void startThread()
    {
        if (thread == null || !thread.IsAlive)
        {
            thread = new Thread(doAuto);
            thread.Start();
        }
    }

    public void doAuto()
    {
        while (!threadStop)
        {
            Thread.Sleep(2000);
            autoKSMo();
            autoHS();
            goHome();
            autoBuff();
            /*if (config.IsAuto)
            {
                autoHS();
                goHome();
                autoThuCuoi();
                autoBuff();
            }*/
        }
    }

    public static void paint(mGraphics g)
    {
        mFont.tahoma_7b_red.drawString(g, "MOD BY HUNGCHOI ", 150, 6, 0, mGraphics.isFalse);
        g.setColor(16514362);
        g.drawRect(140, 0, 107, 22, mGraphics.isFalse);
        mFont.tahoma_8b_black.drawString(g, "Thời Gian: " + DateTime.Now.ToString(), 6, 64, 0, mGraphics.isFalse);
        mFont.tahoma_7b_yellow.drawString(g, GameCanvas.loadmap.nameMap + ", Khu: " + (LoadMap.Area + 1), 5, 100, 0, mGraphics.isFalse);
        mFont.tahoma_7b_yellow.drawString(g, "Kho đồ: " + Item.VecInvetoryPlayer.size() + "/" + Player.maxInven, 5, 110, 0, mGraphics.isFalse);
        mFont.tahoma_7b_yellow.drawString(g, "Vàng: " + GameScreen.player.coin, 5, 120, 0, mGraphics.isFalse);
        mFont.tahoma_7b_yellow.drawString(g, "Ngọc: " + GameScreen.player.gold, 5, 130, 0, mGraphics.isFalse);
        mFont.tahoma_7b_yellow.drawString(g, "Pet food: " + GameScreen.pet != null ? foodPet.ToString() : "Không có Pet", 5, 140, 0, mGraphics.isFalse);
        mFont.tahoma_7b_yellow.drawString(g, "IDMap: " + GameCanvas.loadmap.idMap, 5, 150, 0, mGraphics.isFalse);
        //mFont.tahoma_7_yellow.drawString(g, "typePK: " + GameScreen.player.typePk + ", objFocusPK: " + GameScreen.ObjFocus.typePk + ", typeObjectFocus: " + GameScreen.ObjFocus.typeObject, 5, 160, 0, mGraphics.isFalse);
        if (!isHideInfo)
        {
            return;
        }
        for (int i = 0; i < GameScreen.Vecplayers.size(); i++)
        {
            MainObject mainObject = (MainObject)GameScreen.Vecplayers.elementAt(i);
            if (mainObject.typeObject == 0)
            {
                mFont.tahoma_7_white.drawString(g, mainObject.name + "(" + mainObject.hp + ")", GameCanvas.w - 5, 50 + i * 10, 1, mGraphics.isFalse);
            }
            if (mainObject.typeObject != 1 && mainObject.typeObject == 2)
            {
                if (mainObject.name.Equals("Thiên thạch"))
                {
                    mFont.tahoma_7b_orange.drawString(g, mainObject.name + " (" + mainObject.hp + ")", GameCanvas.w - 5, 50 + i * 10, 1, mGraphics.isFalse);
                }
                else
                {
                    mFont.tahoma_7_blue.drawString(g, mainObject.name, GameCanvas.w - 5, 50 + i * 10, 1, mGraphics.isFalse);
                }
            }
        }
    }

    public static void menuMod()
    {
        mVector mVector3 = new mVector("menu mod");
        mVector3.addElement(pro = new iCommand("Menu Pro", 99, null));
        mVector3.addElement(da1 = new iCommand("Đá Dịch Chuyển LST", 1, null));
        mVector3.addElement(da2 = new iCommand("Đá Dịch Chuyển TPKB", 3, null));
        mVector3.addElement(tansat = new iCommand("Tự Động Đánh Khi Đứng Yên", 6, null));
        mVector3.addElement(dongnl = new iCommand("Đóng Mề Trắng", 10, null));
        mVector3.addElement(khoavitri = new iCommand("Khóa Vị Trí: " + (isKhoaViTri ? "Bật" : "Tắt"), 5, null));
        mVector3.addElement(focus = new iCommand("Khóa Focus", 12, null));
        mVector3.addElement(aths = new iCommand("Auto Hồi Sinh: " + (isAutoHS ? "Bật" : "Tắt"), 8, null));
        mVector3.addElement(dongnl = new iCommand("Thông Báo Boss: " + (isNotifBoss ? "Bật" : "Tắt"), 20, null));
        mVector3.addElement(atpotion = new iCommand("Auto HPMP: " + (isAutoPotion ? "Bật" : "Tắt"), 9, null));
        mVector3.addElement(dongnl = new iCommand("Auto Đăng Nhập", 21, null));
        mVector3.addElement(dongnl = new iCommand("Auto GoBack Khi Chết", 17, null));
        string name = "MENU CHOIMOD";
        GameCanvas.menu2.startAt(mVector3, 2, name, isFocus: false, null);
    }

    public static void commandMenu(int index)
    {
        switch (index)
        {
            case 99:
                {
                    mVector mVector3 = new mVector("menu hut quai");
                    mVector3.addElement(hutMons = new iCommand("Hút Quái Vật", 21, null));
                    mVector3.addElement(hutPlayers = new iCommand("Hút Người Chơi", 20, null));
                    mVector3.addElement(dosat = new iCommand("Đồ Sát: " + (isDoSat ? "Bật" : "Tắt"), 26, null));
                    mVector3.addElement(atksmo = new iCommand("Auto KS Mỏ: " + (isKSMo ? "Bật" : "Tắt"), 24, null));
                    mVector3.addElement(atpb = new iCommand("Auto Phó Bản: " + (isAutoPB ? "Bật" : "Tắt"), 11, null));
                    mVector3.addElement(hutMons = new iCommand("Còn nhiều cái hay nữa...", 0, null));
                    GameCanvas.menu2.startAt(mVector3, 2, "Menu Pro", isFocus: false, null);
                    break;
                }
            case 1:
                GlobalService.gI().getlist_from_npc(-10);
                break;
            case 3:
                GlobalService.gI().getlist_from_npc(-33);
                break;
            case 4:
                isAutoHS = !isAutoHS;
                GameCanvas.addInfoChar("Hồi Sinh Bằng Vật Phẩm: " + (isAutoHS ? "Bật" : "Tắt"));
                if (isAutoHS)
                {
                    isAutoGoHome = false;
                }
                break;
            case 5:
                isKhoaViTri = !isKhoaViTri;
                GameCanvas.addInfoChar("Khoá vị trí: " + (isKhoaViTri ? "Bật" : "Tắt"));
                menuMod();
                break;
            case 6:
                {
                    mVector mVector4 = new mVector("menu ts");
                    iCommand o3 = new iCommand("Tự Động Đánh: " + (isAutoFire ? "Bật" : "Tắt"), 18, null);
                    iCommand o4 = new iCommand("Chọn Quái Đánh", 16, null);
                    mVector4.addElement(o3);
                    mVector4.addElement(o4);
                    GameCanvas.menu2.startAt(mVector4, 2, "Menu Tàn Sát", isFocus: false, null);
                    break;
                }
            case 7:
                isAutoGoHome = !isAutoGoHome;
                GameCanvas.addInfoChar("Auto Về Làng: " + (isAutoGoHome ? "Bật" : "Tắt"));
                if (isAutoGoHome)
                {
                    isAutoHS = false;
                }
                break;
            case 8:
                {
                    mVector mVector5 = new mVector("menu hs");
                    iCommand o5 = new iCommand("Bằng Vật Phẩm: " + (isAutoFire ? "Bật" : "Tắt"), 4, null);
                    iCommand o6 = new iCommand("Bằng Vàng Hoặc Ngọc", 15, null);
                    iCommand o7 = new iCommand("Auto Về Làng", 7, null);
                    mVector5.addElement(o5);
                    mVector5.addElement(o6);
                    mVector5.addElement(o7);
                    GameCanvas.menu2.startAt(mVector5, 2, "Menu Hồi Sinh", isFocus: false, null);
                    break;
                }
            case 9:
                isAutoPotion = !isAutoPotion;
                GameCanvas.addInfoChar("Auto Dùng HPMP Trong Hành Trang: " + (isAutoPotion ? "Bật" : "Tắt"));
                menuMod();
                break;
            case 10:
                GlobalService.gI().Dynamic_Menu(-125, -125, 0);
                break;
            case 11:
                isAutoPB = !isAutoPB;
                GameCanvas.addInfoChar("Auto Phó Bản: " + (isAutoPB ? "Bật" : "Tắt"));
                menuMod();
                break;
            case 12:
                {
                    mVector mVector7 = new mVector("menu focus");
                    iCommand o8 = new iCommand("Hủy Focus", 13, null);
                    iCommand o9 = new iCommand("Focus mục tiêu nhanh: " + GameScreen.ObjFocus.name, 14, null);
                    iCommand o10 = new iCommand("Chọn focus quái", 0, null);
                    iCommand o11 = new iCommand("Chọn focus boss", 0, null);
                    mVector7.addElement(o8);
                    mVector7.addElement(o9);
                    mVector7.addElement(o10);
                    mVector7.addElement(o11);
                    GameCanvas.menu2.startAt(mVector7, 2, "Menu Focus", isFocus: false, null);
                    break;
                }
            case 13:
                isKhoaFocus = false;
                GameScreen.player.nextFocus();
                break;
            case 14:
                objFocus = GameScreen.ObjFocus;
                isKhoaFocus = true;
                break;
            case 16:
                {
                    listMon = new mVector("list monster");
                    HashSet<string> hashSet2 = new HashSet<string>();
                    for (int j = 0; j < GameScreen.Vecplayers.size(); j++)
                    {
                        MainObject mainObject2 = (MainObject)GameScreen.Vecplayers.elementAt(j);
                        if (mainObject2.typeObject == 1 && !hashSet2.Contains(mainObject2.name))
                        {
                            iCommand o2 = new iCommand(mainObject2.name, 19, null);
                            listMon.addElement(o2);
                            hashSet2.Add(mainObject2.name);
                        }
                    }
                    GameCanvas.menu2.startAt(listMon, 2, "Danh Sách Quái", isFocus: false, null);
                    break;
                }
            case 18:
                isAutoFire = !isAutoFire;
                GameCanvas.addInfoChar("Tự động đánh: " + (isAutoFire ? "Bật" : "Tắt"));
                break;
            case 19:
                isAutoFire = true;
                nameMon = ((iCommand)listMon.elementAt(GameCanvas.menu2.menuSelectedItem)).caption;
                GameCanvas.addInfoChar("Tự động đánh: " + nameMon);
                break;
            case 20:
                {
                    listPlayer = new mVector("list player");
                    iCommand o1 = new iCommand("Hút Player: " + (isHutPlayers ? "Bật" : "Tắt"), 27, null);
                    listPlayer.addElement(o1);
                    listPlayers = new HashSet<string>();
                    for (int i = 0; i < GameScreen.Vecplayers.size(); i++)
                    {
                        MainObject mainObject = (MainObject)GameScreen.Vecplayers.elementAt(i);
                        if (mainObject.typeObject == 0 && !listPlayers.Contains(mainObject.name))
                        {
                            iCommand o = new iCommand(mainObject.name, 25, null);
                            listPlayer.addElement(o);
                            listPlayers.Add(mainObject.name);
                        }
                    }
                    GameCanvas.menu2.startAt(listPlayer, 2, "Menu Hút Người Chơi", isFocus: false, null);
                    break;
                }
            case 21:
                {
                    listMon = new mVector("list monster");
                    iCommand o1 = new iCommand("Hút Quái: " + (isHutMons ? "Bật" : "Tắt"), 22, null);
                    listMon.addElement(o1);
                    listMons = new HashSet<string>();
                    for (int i = 0; i < GameScreen.Vecplayers.size(); i++)
                    {
                        MainObject mainObject = (MainObject)GameScreen.Vecplayers.elementAt(i);
                        if (mainObject.typeObject == 1 && !listMons.Contains(mainObject.name))
                        {
                            iCommand o = new iCommand(mainObject.name, 23, null);
                            listMon.addElement(o);
                            listMons.Add(mainObject.name);
                        }
                    }
                    GameCanvas.menu2.startAt(listMon, 2, "Menu Hút Quái", isFocus: false, null);
                    break;
                }
            case 22:
                isHutMons = !isHutMons;
                if (!isHutMons)
                {
                    isHutMonsByName = false;
                    nameMon = "";
                }
                GameScreen.player.strChatPopup = "Hút Quái: " + (isHutMons ? "Bật" : "Tắt");
                break;
            case 23:
                if (!isHutMons)
                {
                    isHutMons = true;
                }
                isHutMonsByName = true;
                nameMon = ((iCommand)listMon.elementAt(GameCanvas.menu2.menuSelectedItem)).caption;
                GameCanvas.addInfoChar("Hút quái: " + nameMon);
                break;
            case 24:

                isKSMo = !isKSMo;
                if (isKSMo)
                {
                    isAutoHS = false;
                }
                else
                {
                    isAutoHS = true;
                }
                GameCanvas.addInfoChar("Auto KS Mỏ: " + (isKSMo ? "Bật" : "Tắt"));
                break;
            case 25:
                if (!isHutPlayers)
                {
                    isHutPlayers = true;
                }
                isHutPlayerByName = true;
                namePlayer = ((iCommand)listPlayer.elementAt(GameCanvas.menu2.menuSelectedItem)).caption;
                GameCanvas.addInfoChar("Hút Người Chơi: " + namePlayer);
                break;
            case 26:
                isDoSat = !isDoSat;
                GameCanvas.addInfoChar("Đồ Sát: " + (isDoSat ? "Bật" : "Tắt"));
                break;
            case 27:
                isHutPlayers = !isHutPlayers;
                if (!isHutPlayers)
                {
                    isHutPlayerByName = false;
                    namePlayer = "";
                }
                GameScreen.player.strChatPopup = "Hút Player: " + (isHutPlayers ? "Bật" : "Tắt");
                break;
            case 2:
            case 15:
            case 17:
                break;
        }
    }

    public static void autoHS()
    {
        if (GameScreen.gI().left != Player.cmdRevice)
        {
            return;
        }
        Thread.Sleep(10000);
        if ((GameScreen.gI().left != Player.cmdRevice && GameScreen.gI().right != Player.cmdVeLang && GameScreen.gI().center != Player.cmdVeLang) || !isAutoHS)
        {
            return;
        }
        Item item = null;
        for (int i = 0; i < Item.VecInvetoryPlayer.size(); i++)
        {
            Item item2 = (Item)Item.VecInvetoryPlayer.elementAt(i);
            if (item2.Id == 61 && item2.ItemCatagory == 4)
            {
                item = item2;
            }
        }
        if (item != null)
        {
            GlobalService.gI().Dynamic_Menu(-51, 0, 0);
            Thread.Sleep(1000);
        }
    }

    public static void autoPotionInv()
    {
        if (!isAutoPotion || GameScreen.player.Action == 4 || GameCanvas.gameTick % 5 != 0)
        {
            return;
        }
        if (GameScreen.player.hp * 100 / GameScreen.player.maxHp < MsgDialog.mHPMP[0])
        {
            Item item = null;
            for (int i = 0; i < Item.VecInvetoryPlayer.size(); i++)
            {
                Item item2 = (Item)Item.VecInvetoryPlayer.elementAt(i);
                if ((item2.Id == 0 || item2.Id == 1 || item2.Id == 2) && item2.ItemCatagory == 4)
                {
                    item = item2;
                }
            }
            GlobalService.gI().Use_Potion((short)item.Id);
            Player.timeDelayPotion[item.typePotion].value = 2000;
            Player.timeDelayPotion[item.typePotion].limit = 2000;
            Player.timeDelayPotion[item.typePotion].timebegin = mSystem.currentTimeMillis();
        }
        if (GameScreen.player.mp * 100 / GameScreen.player.maxMp >= MsgDialog.mHPMP[1])
        {
            return;
        }
        Item item3 = null;
        for (int j = 0; j < Item.VecInvetoryPlayer.size(); j++)
        {
            Item item4 = (Item)Item.VecInvetoryPlayer.elementAt(j);
            if ((item4.Id == 3 || item4.Id == 4 || item4.Id == 5) && item4.ItemCatagory == 4)
            {
                item3 = item4;
            }
        }
        GlobalService.gI().Use_Potion((short)item3.Id);
        Player.timeDelayPotion[item3.typePotion].value = 2000;
        Player.timeDelayPotion[item3.typePotion].limit = 2000;
        Player.timeDelayPotion[item3.typePotion].timebegin = mSystem.currentTimeMillis();
    }

    public static void autoFire(string name)
    {
        if (isAutoFire && !GameScreen.player.isDie)
        {
            if (name == null)
            {
                if (objFocus != null && objFocus.isDie)
                {
                    GameScreen.ObjFocus = null;
                }
                GameScreen.player.nextMonster();
            }
            else
            {
                for (int i = 0; i < GameScreen.Vecplayers.size(); i++)
                {
                    MainObject mainObject = (MainObject)GameScreen.Vecplayers.elementAt(i);
                    if (mainObject == null || mainObject.Action == 4 || mainObject.typeObject != 1 || mainObject.isSend || mainObject.isDie || mainObject.name != name)
                    {
                        if (mainObject != null && mainObject.isDie)
                        {
                            GameScreen.ObjFocus = null;
                        }
                        GameScreen.player.nextMonster();
                    }
                    else
                    {
                        GameScreen.ObjFocus = mainObject;
                        break;
                    }
                }
            }
            Player.isAutoFire = 1;
            Player.isCurAutoFire = true;
        }
    }

    public static void khoaViTri()
    {
        if (isKhoaViTri)
        {
            GameScreen.player.posTransRoad = null;
            GameScreen.player.vx = 0;
            GameScreen.player.vy = 0;
            GameScreen.player.toX = GameScreen.player.x;
            GameScreen.player.toY = GameScreen.player.y;
        }
    }

    public static void autoBuff()
    {
        if (isAutoBuff)
        {
            GlobalService.gI().BuffMore(13, 1, null);
            GlobalService.gI().BuffMore(14, 1, null);
            GlobalService.gI().BuffMore(18, 0, null);
        }
    }

    public static void hutMonster()
    {
        if (!isHutMons)
        {
            return;
        }
        for (int i = 0; i < GameScreen.Vecplayers.size(); i++)
        {
            MainObject mainObject = (MainObject)GameScreen.Vecplayers.elementAt(i);
            if (mainObject.typeObject == 1 && !mainObject.isDie && (!isHutMonsByName || mainObject.name.Equals(nameMon)))
            {
                mainObject.x = GameScreen.player.x + 20;
                mainObject.y = GameScreen.player.y + 20;
                mainObject.vx = 0;
                mainObject.vy = 0;
            }
        }
    }

    public static void autoHutPlayers()
    {
        if (!isHutPlayers)
        {
            return;
        }
        for (int i = 0; i < GameScreen.Vecplayers.size(); i++)
        {
            MainObject mainObject = (MainObject)GameScreen.Vecplayers.elementAt(i);
            if (mainObject.typeObject == 0 && !mainObject.isDie && (!isHutPlayerByName || mainObject.name.Equals(namePlayer)))
            {
                mainObject.x = GameScreen.player.x - 20;
                mainObject.y = GameScreen.player.y - 20;
                mainObject.vx = 0;
                mainObject.vy = 0;
            }
        }
    }

    public static void autoPB()
    {
        if (!isAutoPB || LoadMap.typeMap != LoadMap.MAP_PHO_BANG)
        {
            return;
        }
        int num = 0;
        int num2 = 0;
        mVector mVector3 = new mVector("List Monster");
        for (int i = 0; i < GameScreen.Vecplayers.size(); i++)
        {
            MainObject mainObject = (MainObject)GameScreen.Vecplayers.elementAt(i);
            if (mainObject.name.Equals("Thiên thạch"))
            {
                num = mainObject.x;
                num2 = mainObject.y;
            }
            if (mainObject.typeObject == 1 && !mainObject.isDie)
            {
                mVector3.addElement(mainObject);
            }
        }
        MainObject mainObject2 = null;
        double num3 = 0.0;
        bool flag = false;
        if (mVector3.size() <= 0)
        {
            return;
        }
        for (int j = 0; j < mVector3.size(); j++)
        {
            MainObject mainObject3 = (MainObject)mVector3.elementAt(j);
            double num4 = Mathf.Sqrt((num - mainObject3.x) * (num - mainObject3.x) + (num2 - mainObject3.y) * (num2 - mainObject3.y));
            if (j == 0 || num4 < num3)
            {
                num3 = num4;
                mainObject2 = mainObject3;
            }
            if (BossPB(mainObject3) != null)
            {
                flag = true;
            }
        }
        if (mainObject2 == null)
        {
            return;
        }
        if (isHutMons)
        {
            GameScreen.ObjFocus = mainObject2;
            return;
        }
        if (flag)
        {
            if (!hasUpdatedPlayer)
            {
                GameScreen.player.x = xOld;
                GameScreen.player.y = yOld;
                hasUpdatedPlayer = true;
            }
            GameCanvas.addInfoChar("Đã tắt Tele quái!");
            GameScreen.player.posTransRoad = GameCanvas.game.updateFindRoad(mainObject2.toX / LoadMap.wTile, mainObject2.toY / LoadMap.wTile, GameScreen.player.x / LoadMap.wTile, GameScreen.player.y / LoadMap.wTile, 100);
            GameScreen.xCur = mainObject2.x;
            GameScreen.yCur = mainObject2.y;
            GameScreen.ObjFocus = mainObject2;
            return;
        }
        if (CRes.abs(num - mainObject2.x) <= 50 && CRes.abs(num2 - mainObject2.x) <= 50)
        {
            GameCanvas.addInfoChar("Bật đóng băng quái");
            mainObject2.xStopMove = mainObject2.x;
            mainObject2.yStopMove = mainObject2.y;
            mainObject2.toX = mainObject2.xStopMove;
            mainObject2.toY = mainObject2.yStopMove;
            mainObject2.vx = 0;
            mainObject2.vy = 0;
        }
        hasUpdatedPlayer = false;
        GameScreen.player.posTransRoad = null;
        GameScreen.player.x = mainObject2.toX;
        GameScreen.player.y = mainObject2.toY;
    }

    public static MainObject BossPB(MainObject obj)
    {
        foreach (string item in new HashSet<string> { "Cây Quái Vật", "Ong Khổng Lồ", "Sói Bất Tử", "Thủy Quái" })
        {
            if (obj.name.Equals(item))
            {
                return obj;
            }
        }
        return null;
    }

    public static void khoaFocus(mGraphics g)
    {
        if (isKhoaFocus)
        {
            GameScreen.ObjFocus = objFocus;
            g.setColor(16514362);
            g.drawLine(GameScreen.player.x, GameScreen.player.y, objFocus.x, objFocus.y, useClip: true);
        }
    }

    public static void goHome()
    {
        if (GameScreen.gI().right == Player.cmdVeLang && isAutoGoHome)
        {
            Thread.Sleep(2000);
            if (GameScreen.gI().left == Player.cmdRevice || GameScreen.gI().right == Player.cmdVeLang || GameScreen.gI().center == Player.cmdVeLang)
            {
                GlobalService.gI().gohome(0);
            }
        }
    }

    public static void autoPetEat()
    {
        for (int i = 0; i < Item.VecInvetoryPlayer.size(); i++)
        {
            Item item = (Item)Item.VecInvetoryPlayer.elementAt(i);
            if (item.ItemCatagory == 3)
            {
                GlobalService.gI().Pet_Eat((short)GameScreen.pet.ID, (short)item.Id, (sbyte)item.ItemCatagory, 1);
            }
        }
    }

    public static void sendChat()
    {
        string qLTK = config.Chat;
        GameScreen.player.strChatPopup = qLTK;
        GlobalService.gI().chatPopup(qLTK);
    }

    public static void getItemInv()
    {
        for (int i = 0; i < Item.VecInvetoryPlayer.size(); i++)
        {
            Item item2 = (Item)Item.VecInvetoryPlayer.elementAt(i);
            Rms.writeLog($"Item 1: id: {item2.Id}, name: {item2.itemName}");
        }
    }

    public static string FormatNumber(long number)
    {
        if (number >= 1000000)
        {
            return ((double)number / 1000000.0).ToString("0.##") + "m";
        }
        if (number >= 1000)
        {
            return ((double)number / 1000.0).ToString("0.##") + "k";
        }
        return number.ToString();
    }

    public static void autoThuCuoi()
    {
        if (config.IsAutoNgua)
        {
            for (int i = 0; i < Item.VecInvetoryPlayer.size(); i++)
            {
                Item item2 = (Item)Item.VecInvetoryPlayer.elementAt(i);
                if (config.ComboNgua == item2.itemName)
                {
                    if (GameScreen.player.Action != 4)
                    {
                        sbyte id = (sbyte)item2.Id;
                        GlobalService.gI().Use_Potion(id);
                    }
                }
            }

        }
    }

    public static void UpdateConfig(Action<AccountConfig> updateAction)
    {
        var config = configManager.GetConfig(accountId) ?? new AccountConfig();
        updateAction(config);
        // Cập nhật cấu hình
        configManager.SetConfig(accountId, config);
        // Lưu cấu hình vào file
        configManager.Save(configFilePath);
    }

    public static void autoKSMo()
    {
        if (!isKSMo || GameCanvas.loadmap.idMap != 42)
        {
            return;
        }
        if (GameScreen.player.isDie || GameScreen.player.Action == 4)
        {
            GlobalService.gI().gohome(0);
            Thread.Sleep(1000);
        }

        if (GameScreen.isMapLang)
        {
            GlobalService.gI().getlist_from_npc(-33);
            GlobalService.gI().Dynamic_Menu(-33, 0, 1);
            Thread.Sleep(500);
        }
        Point pointMoNgoc = new Point();
        if (GameCanvas.loadmap.idMap == 22)
        {
            for (int i = 0; i < LoadMap.vecPointChange.size(); i++)
            {
                Point point = (Point)LoadMap.vecPointChange.elementAt(i);
                if (point.name.Contains("Đèo hoang sơ"))
                {
                    pointMoNgoc = point;
                    break;
                }
            }
            GameScreen.player.posTransRoad = GameCanvas.game.updateFindRoad(GameScreen.player.x / LoadMap.wTile, GameScreen.player.y / LoadMap.wTile, (GameScreen.player.x - 50) / LoadMap.wTile, (GameScreen.player.y - 50) / LoadMap.wTile, 20);
            Thread.Sleep(1000);
        }
        if (LoadMap.Area != 4)
        {
            GlobalService.gI().Change_Area(4);
            Thread.Sleep(500);
        }
        MainObject objMo = new MainObject();
        MainObject objNhanBan = new MainObject();
        MainObject mainObject1 = new MainObject();
        for (int i = 0; i < GameScreen.Vecplayers.size(); i++)
        {
            MainObject mainObject = (MainObject)GameScreen.Vecplayers.elementAt(i);
            if (mainObject.name.StartsWith("Mỏ"))
            {
                objMo = mainObject;
                break;
            }
        }

        if (objMo != null)
        {
            objMo.x = GameScreen.player.x - 20;
            objMo.y = GameScreen.player.y - 20;
            GameScreen.ObjFocus = objMo;
            Player.isAutoFire = 1;
            Player.isCurAutoFire = true;
        }
    }

    public static void autoDoSat()
    {
        if (!isDoSat) { return; }
        MainObject objFocus = GameScreen.ObjFocus;
        if (objFocus == null) { return; }
        if (objFocus.isDie || objFocus.Action == 4) { return; }
        if (objFocus.typeObject != 0)
        {
            if (objFocus.typeObject == 1 && objFocus.typeObject != 2)
            {
                GameScreen.ObjFocus = null;
            }
            GameScreen.player.nextFocus();
        }
        if (GameScreen.player.typePk >= 0 && objFocus.name != "Khu")
        {
            GameCanvas.menu2.doCloseMenu();
            GameScreen.player.dofire();
        }

        if (GameScreen.player.typePk == -1 && GameScreen.ObjFocus.typePk == 0 && objFocus.name != "Khu")
        {
            GameCanvas.menu2.doCloseMenu();
            GameScreen.player.dofire();
        }
        if (GameScreen.player.typePk == -1 && GameScreen.ObjFocus.typePk == 10 && objFocus.name != "Khu")
        {
            GameCanvas.menu2.doCloseMenu();
            GameScreen.player.dofire();
        }
        if (GameScreen.player.typePk == -1 && objFocus.typePk == -1)
        {
            GameScreen.player.nextFocus();
        }
    }

    public static AutoManager instance;

    private Thread thread;

    public static string filePath = ".\\auto.txt";

    public static iCommand pro;

    public static iCommand da1;

    public static iCommand da2;

    public static iCommand aths;

    public static iCommand atpotion;

    public static iCommand tansat;

    public static iCommand atoffwhite;

    public static iCommand atlogin;

    public static iCommand khoavitri;

    public static iCommand goback;

    public static iCommand atbuff;

    public static iCommand combo1;

    public static iCommand dongnl;

    public static iCommand atpb;

    public static iCommand focus;

    public static iCommand hutMons;

    public static iCommand chonHutMons;

    public static iCommand chonHutPlayer;

    public static iCommand hutPlayers;

    public static iCommand atksmo;

    public static iCommand dosat;

    private bool threadStop;

    private FileSystemWatcher fileWatcher;

    public static int xOld;

    public static int yOld;

    public static short foodPet;

    public static bool isHutMonsByName = false;

    public static bool isAutoHS = true;

    public static bool isPhanThan = false;

    public static bool isAutoPotion = true;

    public static bool isAutoFire = false;

    public static bool isKhoaViTri;

    public static bool isHideInfo = true;

    public static bool isAutoBuff = true;

    public static bool isAutoPB = false;

    public static bool isTeleMonster = false;

    public static bool isHutMons = false;

    public static bool isKhoaFocus = false;

    public static bool isShowBoss = true;

    public static bool isAutoGoHome = false;

    public static bool isPetEat = false;

    public static bool isHutPlayers = false;

    public static bool isHutPlayerByName = false;

    public static string nameMon;

    public static string namePlayer;

    private static MainObject objFocus;

    public static mVector listMon;

    public static mVector listPlayer;

    public static string infoBoss;

    public static int typeBoss;

    public static string flagTime;

    public static bool isNotifBoss = true;

    private static bool hasNewBossNotification = false;

    public static bool flagToughLv;

    public static bool hasUpdatedPlayer = false;

    public static bool isHideNameOtherChar = false;

    public static bool isKSMo = false;

    public static bool isDoSat = false;

    public static string[] array5 = Environment.GetCommandLineArgs()[1].Split('|');

    public static ConfigManager configManager;

    public const string configFilePath = "config.json";

    public static string accountId;

    public static AccountConfig config;

    public static bool isHutItem = true;

    public static HashSet<string> listMons;

    public static HashSet<string> listPlayers;

    public static int indexSkill = 1;
}
