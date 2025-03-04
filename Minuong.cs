public class Minuong : MainMonster
{
	public sbyte Maxspeed;

	private sbyte typemove;

	public sbyte[][][] FRAMEMOVE = new sbyte[3][][]
	{
		new sbyte[4][]
		{
			new sbyte[13]
			{
				7, 7, 7, 8, 8, 8, 9, 9, 9, 6,
				6, 6, 6
			},
			new sbyte[13]
			{
				12, 12, 12, 13, 13, 13, 14, 14, 14, 11,
				11, 11, 11
			},
			new sbyte[13]
			{
				2, 2, 2, 3, 3, 3, 4, 4, 4, 1,
				1, 1, 1
			},
			new sbyte[13]
			{
				2, 2, 2, 3, 3, 3, 4, 4, 4, 1,
				1, 1, 1
			}
		},
		new sbyte[4][]
		{
			new sbyte[16]
			{
				7, 7, 7, 7, 8, 8, 8, 8, 9, 9,
				9, 9, 6, 6, 6, 6
			},
			new sbyte[16]
			{
				12, 12, 12, 12, 13, 13, 13, 13, 14, 14,
				14, 14, 11, 11, 11, 11
			},
			new sbyte[16]
			{
				2, 2, 2, 2, 3, 3, 3, 3, 4, 4,
				4, 4, 1, 1, 1, 1
			},
			new sbyte[16]
			{
				2, 2, 2, 2, 3, 3, 3, 3, 4, 4,
				4, 4, 1, 1, 1, 1
			}
		},
		new sbyte[4][]
		{
			new sbyte[16]
			{
				7, 7, 7, 7, 8, 8, 8, 8, 9, 9,
				9, 9, 6, 6, 6, 6
			},
			new sbyte[16]
			{
				12, 12, 12, 12, 13, 13, 13, 13, 14, 14,
				14, 14, 11, 11, 11, 11
			},
			new sbyte[16]
			{
				2, 2, 2, 2, 3, 3, 3, 3, 4, 4,
				4, 4, 1, 1, 1, 1
			},
			new sbyte[16]
			{
				2, 2, 2, 2, 3, 3, 3, 3, 4, 4,
				4, 4, 1, 1, 1, 1
			}
		}
	};

	public sbyte[][] FRAMESTAND = new sbyte[4][]
	{
		new sbyte[21]
		{
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 6, 6, 6, 6, 6, 6, 6, 6, 6,
			6
		},
		new sbyte[20]
		{
			10, 10, 10, 10, 10, 10, 10, 10, 10, 10,
			11, 11, 11, 11, 11, 11, 11, 11, 11, 11
		},
		new sbyte[20]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			1, 1, 1, 1, 1, 1, 1, 1, 1, 1
		},
		new sbyte[20]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			1, 1, 1, 1, 1, 1, 1, 1, 1, 1
		}
	};

	public sbyte[] dysai = new sbyte[3] { -11, -7, -7 };

	private sbyte numW;

	private sbyte numH;

	private sbyte vTam;

	private sbyte xwater;

	public Minuong(int ID, int Monster, int typeMonster, string name, int x, int y, int maxHP, int lv)
	{
		base.typeMonster = typeMonster;
		base.ID = ID;
		catalogyMonster = Monster;
		typeObject = 1;
		base.name = name;
		base.x = x;
		base.y = y;
		hOne = -1;
		wOne = -1;
		maxHp = maxHP;
		Lv = (short)lv;
		typemove = getTypemove();
		Action = 0;
		MonWater = 0;
		numW = 3;
		numH = 5;
		nFrame = numW * numH;
		timeLoadInfo = mSystem.currentTimeMillis();
	}

	public override bool isMiNuong()
	{
		return true;
	}

	public override void setspeedVantieu(int vmax)
	{
		vMax = vmax;
		Maxspeed = (sbyte)vmax;
		vTam = (sbyte)(vmax + 1);
	}

	public new string getnameOwner()
	{
		return nameowner;
	}

	protected sbyte getTypemove()
	{
		return 2;
	}

	public override void paint(mGraphics g)
	{
		MainImage imagePartMonster = ObjectData.getImagePartMonster((short)catalogyMonster);
		if (imagePartMonster != null && !isDongBang && imagePartMonster.img != null)
		{
			if (Direction == 3)
			{
				xwater = 2;
			}
			else if (Direction == 2)
			{
				xwater = -2;
			}
			else
			{
				xwater = 0;
			}
			if (wOne < 0)
			{
				wOne = mImage.getImageWidth(imagePartMonster.img.image) / numW;
				hOne = mImage.getImageHeight(imagePartMonster.img.image) / numH;
				yEffAuto = hOne / 2;
			}
			if (frameMount >= 0 && frameMount < nFrame)
			{
				int num = 0;
				int num2 = 0;
				num = frameMount / numH * wOne;
				num2 = frameMount % numH * hOne;
				if (isWater)
				{
					g.drawRegion(imagePartMonster.img, num, num2, wOne, hOne, (Direction > 2) ? 2 : 0, x, y - ysai - dy + dyWater, mGraphics.BOTTOM | mGraphics.HCENTER, mGraphics.isTrue);
					g.drawRegion(MainObject.water, 0, ((Action != 0) ? 2 : 0) * 17 + GameCanvas.gameTick / 2 % 2 * 17, 28, 17, 0, x + xwater, y + dyWater - 8, 3, mGraphics.isFalse);
				}
				else
				{
					MainObject.paintShadow(g, Direction, x + xwater, y - ysai + dysai[typemove]);
					g.drawRegion(imagePartMonster.img, num, num2, wOne, hOne, (Direction > 2) ? 2 : 0, x, y - ysai - dy + dyWater, mGraphics.BOTTOM | mGraphics.HCENTER, mGraphics.isTrue);
				}
			}
		}
		mFont.tahoma_7_white.drawString(g, (!nameowner.Equals(string.Empty)) ? nameowner : name, x, y - hOne - 8 - (isDongBang ? 5 : 0), 2, mGraphics.isFalse);
		base.paint(g);
	}

	public void updateMonsterAction()
	{
		fMount++;
		if (Action == 0)
		{
			if (fMount > FRAMESTAND[Direction].Length - 1)
			{
				fMount = 0;
			}
			frameMount = FRAMESTAND[Direction][fMount];
		}
		else if (Action == 1)
		{
			if (fMount > FRAMEMOVE[typemove][Direction].Length - 1)
			{
				fMount = 0;
			}
			frameMount = FRAMEMOVE[typemove][Direction][fMount];
		}
	}

	public override void update()
	{
		base.update();
		updateMonsterAction();
		int tile = GameCanvas.loadmap.getTile(x + vx, y + vy);
		setMove(MonWater, tile);
		if (!Canmove() || isBinded)
		{
			return;
		}
		if (!isInfo)
		{
			long num = mSystem.currentTimeMillis();
			if (num - timeLoadInfo >= 5000)
			{
				timeLoadInfo = num;
				GlobalService.gI().monster_info((short)ID);
			}
		}
		if (vx == 0 && vy == 0)
		{
			Action = 0;
		}
		if (MainObject.getDistance(x, y, toX, toY) >= LoadMap.wTile * 3 && MainObject.getDistance(x, y, toX, toY) <= LoadMap.wTile * 4)
		{
			Maxspeed = vTam;
		}
		if (MainObject.getDistance(x, y, toX, toY) < LoadMap.wTile * 3)
		{
			vMax = (byte)(vTam - 1);
		}
		if (Maxspeed > 1)
		{
			if (frameMount == 3 || frameMount == 8 || frameMount == 13)
			{
				vMax = Maxspeed - 1;
			}
			else
			{
				vMax = Maxspeed;
			}
		}
	}

	public override void SetnameOwner(string name)
	{
		nameowner = name;
	}
}
