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
		cout << "�÷��̾� �̸� : " << name << '\n';
		cout << "ä�� : " << health << '\n';
		cout << "���ݷ� : " << attack << '\n';
		cout << "��ų ���ݷ� : " << skill << '\n';
		cout << "���� �ż� : " << gold << '\n';
		cout << "���� ǥâ �� : " << dart << '\n';
		cout << "���� ����(ȸ����) : " << position << '\n';
	}

	void Attack(unit& target) override {
		int minattack = attack / 2;
		int maxattack = attack * 2;
		int randomattack = rand() % (maxattack - minattack + 1) + minattack;
		cout << name << "��(��) ǥâ�� �����ϴ�" << '\n';
		dart = --dart;
		if (dart <= 0) {
			cout << "ǥâ�� ��� ������ �� �� �����ϴ� " << '\n';
			return;
		}
		target.Sethealth(target.Gethealth() - randomattack);
		cout << "�������� " << randomattack << "��ŭ �޾ҽ��ϴ�" << "�� ä���� "
			<< target.Gethealth() << "���ҽ��ϴ�" << '\n';
		cout << "���� ǥâ ���� " << dart << "���ҽ��ϴ� " << '\n';

	}
	void Skill(unit& target) override {
		int minskill = skill / 2;
		int maxskill = skill * 2;
		int randomskill = rand() % (maxskill - minskill + 1) + minskill;
		cout << name << "��(��) ��ų�� ��� �մϴ�" << '\n';
		dart = --dart;
		if (dart <= 0) {
			cout << "ǥâ�� ��� ��ų�� ����� �� �����ϴ� " << '\n';
			return;
		}
		target.Sethealth(target.Gethealth() - randomskill);
		string skill_name[4] = { "��Ű����","Ʈ���� ���ο�", "������ ÿ����","��� ���̵�" };
		cout << skill_name[rand() % 4] << "�� ����߽��ϴ�" << '\n';
		cout << "��ų �������� " << randomskill << "��ŭ �޾ҽ��ϴ� " << "�� ä���� "
			<< target.Gethealth() << "���ҽ��ϴ�" << '\n';
		cout << "���� ǥâ ���� " << dart << "���ҽ��ϴ� " << '\n';
	}
	void HP() override {
		if (position > 0)
		{
			int hpamount = 100;
			health += hpamount;
			position--;
			cout << name << "��(��) �Ͼ������� ����߽��ϴ�." << '\n';
			cout << "���� �Ͼ������� ������ " << position << "�Դϴ�" << '\n';
		}
		else
		{
			cout << "��� ������ ����� ���߽��ϴ�" << '\n';
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
		cout << "���� �̸� : " << name << '\n';
		cout << "ä�� : " << health << '\n';
		cout << "���ݷ� : " << attack << '\n';
		cout << "��ų ���ݷ� : " << skill << '\n';
	}

	void Attack(unit& target) override {
		cout << name << "�� ������ �մϴ� ���� �ϼ���!!!!" << '\n';
		target.Sethealth(target.Gethealth() - attack);
		cout << "�������� " << attack << "��ŭ �޾ҽ��ϴ� " << "�÷��̾� ü���� " << target.Gethealth()
			<< "���ҽ��ϴ� " << '\n';

	}

	void Skill(unit& target) override {
		cout << name << "�� ������ ������ �մϴ� ���� �ϼ���!!!!" << '\n';
		target.Sethealth(target.Gethealth() - skill);
		cout << "�������� " << skill << "��ŭ �޾ҽ��ϴ� " << "�÷��̾� ü���� " << target.Gethealth()
			<< "���ҽ��ϴ� " << '\n';
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
		cout << "Ű���� ������ �Է��� �ּ���(�� W, �Ʒ� S, ������ D, ���� A) " << '\n';
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
		cout << "���͸� �������ϴ� ��Ʋ�� ���� �մϴ�" << '\n';
		thief t("����", 500, 50, 70, 10, 5000, 20);
		monster z("������ ��������", 250, 60, 80, 0);
		battle_event(t, z);
	}
	if (mapx == 16 && mapy == 6 || mapx == 18 && mapy == 15)
	{
		cout << "���͸� �������ϴ� ��Ʋ�� ���� �մϴ�" << '\n';
		thief t("����", 500, 50, 70, 10, 5000, 20);
		monster z("��ޱ��A", 320, 70, 80, 0);
		battle_event(t, z);
	}
}

void slidemap::battle_event(thief& t, monster& z)
{
	while (t.Gethealth() > 0 && z.Gethealth() > 0)
	{
		int m_input = rand() % 2 + 1;
		cout << "================" << '\n';
		cout << "����� ���� �Դϴ�" << '\n';
		cout << "================" << '\n';
		Sleep(3000);
		t.status();
		cout << '\n';
		z.status();
		cout << '\n';
		cout << "====================" << '\n';
		cout << "1.���� 2.��ų 3.ȸ��" << '\n';
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
				cout << "�߸� �Է� �ϼ̽��ϴ� " << '\n';
				Sleep(3000);
				cout << "�ٽ� �Է� �ϼ���" << '\n';
				continue;
			}
		}
		if (z.Gethealth() <= 0)
		{
			cout << "�¸� �Ͽ����ϴ�" << '\n';
			Sleep(3000);
			cout << "������ ���� �մϴ�" << '\n';
			break;
		}
		switch (m_input)
		{
		case 1:
			cout << "================" << '\n';
			cout << "���� ���� �Դϴ�" << '\n';
			cout << "================" << '\n';
			Sleep(3000);
			z.Attack(t);
			Sleep(3000);
			break;
		case 2:
			cout << "================" << '\n';
			cout << "���� ���� �Դϴ�" << '\n';
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
			cout << "���ӿ� �����ϴ�" << '\n';
			Sleep(3000);
			cout << "������ ���� �մϴ�" << '\n';
			break;
		}

	}
}