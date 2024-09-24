#include <iostream>
#include <windows.h>
#include <string>
#include <conio.h>
using namespace std;

class thief;
class monster;

class slidemap
{
private:
	char** map;
	int mapx, mapy;
public:
	void reset();
	void make_map();
	void W_w();
	void S_s();
	void A_a();
	void D_d();
	void dungeon();
	void battle_event(thief& t, monster& z);
	slidemap();
	~slidemap();


};

slidemap::slidemap() : mapx(23), mapy(1)
{

}

slidemap::~slidemap()
{
	for (int i = 0; i < 25; i++)
	{
		delete[] map[i];
		map[i] = nullptr;
	}
	delete[] map;
	map = nullptr;
}


class unit
{
protected:
	int health;
	int attack;
	int mp;
	int skill;
	int gold;
	string name;
public:
	void Sethealth(int a) { health = a; }
	void Setattack(int b) { attack = b; }
	void Setskill(int c) { skill = c; }
	void Setgold(int g) { gold = g; }
	void AddGold(int g) { gold += g; }
	void SubtractGold(int g) { gold -= g; }
	int Gethealth() const { return health; }
	int Getattack() const { return attack; }
	int Getskill() const { return skill; }
	int Getgold() const { return gold; }
	unit(const string& n, const int a, const int b, const int c, int g)
		:name(n), health(a), attack(b), skill(c), gold(g) {
	};
	virtual ~unit() {
	};

	virtual void Attack(unit& target) = 0;
	virtual void Skill(unit& target) = 0;
	virtual void HP() = 0;
	virtual void status() = 0;


};

class thief : public unit
{
private:
	int position;
	int dart;
public:
	thief(const string& n, const int a, const int b, const int c, int p, int g, int d)
		: unit(n, a, b, c, g), position(p), dart(d)
	{

	}
	~thief()
	{

	}

	void status() override
	{
		cout << "플레이어 이름 : " << name << '\n';
		cout << "채력 : " << health << '\n';
		cout << "공격력 : " << attack << '\n';
		cout << "스킬 공격력 : " << skill << '\n';
		cout << "현재 매소 : " << gold << '\n';
		cout << "현재 표창 수 : " << dart << '\n';
		cout << "현재 물약(회복약) : " << position << '\n';
	}

	void Attack(unit& target) override {
		int minattack = attack / 2;
		int maxattack = attack * 2;
		int randomattack = rand() % (maxattack - minattack + 1) + minattack;
		cout << name << "이(가) 표창을 던집니다" << '\n';
		dart = --dart;
		if (dart <= 0) {
			cout << "표창이 없어서 공격을 할 수 없습니다 " << '\n';
			return;
		}
		target.Sethealth(target.Gethealth() - randomattack);
		cout << "데미지는 " << randomattack << "만큼 받았습니다" << "적 채력은 "
			<< target.Gethealth() << "남았습니다" << '\n';
		cout << "현재 표창 수는 " << dart << "남았습니다 " << '\n';

	}
	void Skill(unit& target) override {
		int minskill = skill / 2;
		int maxskill = skill * 2;
		int randomskill = rand() % (maxskill - minskill + 1) + minskill;
		cout << name << "이(가) 스킬을 사용 합니다" << '\n';
		dart = --dart;
		if (dart <= 0) {
			cout << "표창이 없어서 스킬을 사용할 수 없습니다 " << '\n';
			return;
		}
		target.Sethealth(target.Gethealth() - randomskill);
		string skill_name[4] = { "럭키세븐","트리플 스로우", "슈리켄 첼린지","써든 레이드" };
		cout << skill_name[rand() % 4] << "를 사용했습니다" << '\n';
		cout << "스킬 데미지는 " << randomskill << "만큼 받았습니다 " << "적 채력은 "
			<< target.Gethealth() << "남았습니다" << '\n';
		cout << "현재 표창 수는 " << dart << "남았습니다 " << '\n';
	}
	void HP() override {
		if (position > 0)
		{
			int hpamount = 100;
			health += hpamount;
			position--;
			cout << name << "이(가) 하얀포션을 사용했습니다." << '\n';
			cout << "남은 하얀포션의 개수는 " << position << "입니다" << '\n';
		}
		else
		{
			cout << "햐얀 포션을 사용을 다했습니다" << '\n';
		}

	}

};

class monster : public unit
{
public:
	monster(const string& n, const int a, const int b, const int c, int g)
		: unit(n, a, b, c, g)
	{

	}
	~monster()
	{

	}

	void status() override
	{
		cout << "몬스터 이름 : " << name << '\n';
		cout << "채력 : " << health << '\n';
		cout << "공격력 : " << attack << '\n';
		cout << "스킬 공격력 : " << skill << '\n';
	}

	void Attack(unit& target) override {
		cout << name << "이 공격을 합니다 주의 하세요!!!!" << '\n';
		target.Sethealth(target.Gethealth() - attack);
		cout << "데미지는 " << attack << "만큼 받았습니다 " << "플레이어 체력은 " << target.Gethealth()
			<< "남았습니다 " << '\n';

	}

	void Skill(unit& target) override {
		cout << name << "이 강력한 공격을 합니다 주의 하세요!!!!" << '\n';
		target.Sethealth(target.Gethealth() - skill);
		cout << "데미지는 " << skill << "만큼 받았습니다 " << "플레이어 체력은 " << target.Gethealth()
			<< "남았습니다 " << '\n';
	}

	void HP() override {

	}


};

int main()
{
	slidemap* s = new slidemap();
	s->reset();
	while (1)
	{
		s->make_map();
		cout << "키보드 방향을 입력해 주세요(위 W, 아래 S, 오른쪽 D, 왼쪽 A) " << '\n';
		char input_play = _getch();
		switch (input_play)
		{
		case 'W':
		case 'w':
			s->W_w();
			break;
		case 'S':
		case 's':
			s->S_s();
			break;
		case 'D':
		case 'd':
			s->D_d();
			break;
		case 'A':
		case 'a':
			s->A_a();
		default:
			break;
		}
		system("cls");
	}


	delete s;
	return 0;
}

void slidemap::reset()
{
	map = new char* [25];
	for (int i = 0; i < 25; i++)
	{
		map[i] = new char[25];
	}

	char reset_map[25][25] = { {"111111111111111111111111"},
							   {"100000000000000000000001"},
							   {"100000000000000111000001"},
							   {"100000000000000111000001"},
							   {"10000111000000011100d001"},
							   {"100001110000000000000001"},
							   {"100001110000000000000001"},
							   {"100001110000000000000001"},
							   {"100000000000000000000001"},
							   {"100000000000000001100001"},
							   {"100000000011100001100001"},
							   {"100000d00011000000000001"},
							   {"100000000011110000000001"},
							   {"100001110000000000d00001"},
							   {"100001110000000000000001"},
							   {"100001110000001110000001"},
							   {"100000d00000001110000001"},
							   {"100000000000000000000001"},
							   {"100000000000000d00000001"},
							   {"100000001100000000110001"},
							   {"100000001100000000110001"},
							   {"100000000000000000110001"},
							   {"100000000000000000000001"},
							   {"1p00000000000000000000t1"},
							   {"111111111111111111111111"}

	};
	for (int i = 0; i < 25; i++)
	{
		for (int j = 0; j < 25; j++)
		{
			map[i][j] = reset_map[i][j];
		}
	}
}

void slidemap::make_map()
{
	for (int i = 0; i < 25; i++)
	{
		for (int j = 0; j < 25; j++)
		{
			char temp = map[i][j];
			if (temp == '0')
			{
				cout << " ";
			}
			else if (temp == 'p')
			{
				cout << "@";
			}
			else if (temp == '1')
			{
				cout << "#";
			}
			else if (temp == 'd')
			{
				cout << "?";
			}
			else if (temp == 't')
			{
				cout << "!";
			}
		}
		cout << '\n';
	}
}

void slidemap::W_w()
{
	if (mapx > 0 && map[mapx - 1][mapy] != '1')
	{
		int temp = map[mapx][mapy];
		map[mapx][mapy] = map[mapx - 1][mapy];
		map[mapx - 1][mapy] = temp;
		mapx--;
		dungeon();
	}
}

void slidemap::S_s()
{
	if (mapx < 24 && map[mapx + 1][mapy] != '1')
	{
		int temp = map[mapx][mapy];
		map[mapx][mapy] = map[mapx + 1][mapy];
		map[mapx + 1][mapy] = temp;
		mapx++;
		dungeon();
	}
}

void slidemap::A_a()
{
	if (mapy > 0 && map[mapx][mapy - 1] != '1')
	{
		int temp = map[mapx][mapy];
		map[mapx][mapy] = map[mapx][mapy - 1];
		map[mapx][mapy - 1] = temp;
		mapy--;
		dungeon();
	}
}

void slidemap::D_d()
{
	if (mapy < 24 && map[mapx][mapy + 1] != '1')
	{
		int temp = map[mapx][mapy];
		map[mapx][mapy] = map[mapx][mapy + 1];
		map[mapx][mapy + 1] = temp;
		mapy++;
		dungeon();

	}
}

void slidemap::dungeon()
{
	if (mapx == 4 && mapy == 20 || mapx == 11 && mapy == 6)
	{
		cout << "몬스터를 만났습니다 배틀을 시작 합니다" << '\n';
		thief t("도적", 500, 50, 70, 10, 5000, 20);
		monster z("변형된 리본돼지", 250, 60, 80, 0);
		battle_event(t, z);
	}
	if (mapx == 16 && mapy == 6 || mapx == 18 && mapy == 15)
	{
		cout << "몬스터를 만났습니다 배틀을 시작 합니다" << '\n';
		thief t("도적", 500, 50, 70, 10, 5000, 20);
		monster z("상급기사A", 320, 70, 80, 0);
		battle_event(t, z);
	}
}

void slidemap::battle_event(thief& t, monster& z)
{
	while (t.Gethealth() > 0 && z.Gethealth() > 0)
	{
		int m_input = rand() % 2 + 1;
		cout << "================" << '\n';
		cout << "사용자 순서 입니다" << '\n';
		cout << "================" << '\n';
		Sleep(3000);
		t.status();
		cout << '\n';
		z.status();
		cout << '\n';
		cout << "====================" << '\n';
		cout << "1.공격 2.스킬 3.회복" << '\n';
		cout << "====================" << '\n';
		int input;
		cin >> input;
		switch (input)
		{
		case 1:
			t.Attack(z);
			Sleep(3000);
			break;
		case 2:
			t.Skill(z);
			Sleep(3000);
			break;
		case 3:
			t.HP();
			break;
		default:
			if (input != 1 && input != 2 && input != 3)
			{
				cout << "잘못 입력 하셨습니다 " << '\n';
				Sleep(3000);
				cout << "다시 입력 하세요" << '\n';
				continue;
			}
		}
		if (z.Gethealth() <= 0)
		{
			cout << "승리 하였습니다" << '\n';
			Sleep(3000);
			cout << "게임을 종료 합니다" << '\n';
			break;
		}
		switch (m_input)
		{
		case 1:
			cout << "================" << '\n';
			cout << "몬스터 순서 입니다" << '\n';
			cout << "================" << '\n';
			Sleep(3000);
			z.Attack(t);
			Sleep(3000);
			break;
		case 2:
			cout << "================" << '\n';
			cout << "몬스터 순서 입니다" << '\n';
			cout << "================" << '\n';
			Sleep(3000);
			z.Skill(t);
			Sleep(3000);
			break;
		default:
			break;
		}
		if (t.Gethealth() <= 0)
		{
			cout << "게임에 졌습니다" << '\n';
			Sleep(3000);
			cout << "게임을 종료 합니다" << '\n';
			break;
		}

	}
}