
#include <iostream>
#include <windows.h>
#include <iomanip>
#include <GL\freeglut.h>
#include <GL\glut.h>
#include <GL\GL.h>
#include <GL\GLU.h>
#include <Vector>
#include <GL\glui.h>


using namespace std;
int NUMPOINTS = 0;
int NUMPOINTS2 = 0;
bool RMBPressed = false;
int ox, oy, j;
int width = 1000;
int height = 600;
int   main_window;
float color[4] = { 0.0, 0.392157, 0.0, 1.0 };

//Клас нужен за въвеждане на точките, които се запаметяват в масив
class Point {
public:
	GLfloat x, y, z;
	void setxy(GLfloat x2, GLfloat y2) { x = x2; y = y2; }
	const Point & operator=(const Point &rPoint) {
		x = rPoint.x;
		y = rPoint.y;
		z = rPoint.z;

		return *this;
	}

};

vector<Point> abc;
Point *abb;

Point hodograph[1];
vector<Point> hodo;
Point *hodoy;




void myGlutIdle(void)
{

	if (glutGetWindow() != main_window)
		glutSetWindow(main_window);

	glutPostRedisplay();
}


//Рекурсивен метод за изчисляване на крива на Безие с алгоритъма на de Casteljau
Point Bezier(float r, float i, double t, int k)
{
	Point P;
	if (r == 0) {
		if (k == 1)
		{
			return abc[i];
		}
		else 
		{
			return hodo[i];
		}
	}

	Point p1 = Bezier(r - 1, i, t, k);
	Point p2 = Bezier(r - 1, i + 1, t, k);

	P.x = (float)((1 - t) * p1.x + t * p2.x);
	P.y = (float)((1 - t) * p1.y + t * p2.y);
	P.z = (float)((1 - t) * p1.z + t * p2.z);
	return P;

}


//Метод за изчертаване на крива на Безие
void drawLine(Point p1, Point p2) {

	glLineWidth(4);
	glColor4f(0.545098f, 0, 0, 1.0f);

	glEnable(GL_BLEND);
	glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
	glEnable(GL_LINE_SMOOTH);
	glHint(GL_LINE_SMOOTH_HINT, GL_NICEST);
	glBegin(GL_LINE_STRIP);
	glVertex3f(p1.x, p1.y, -0.2);
	glVertex3f(p2.x, p2.y, -0.2);
	glEnd();

	
	glFlush();
}


//Самият метод в който се извикват Bezier и drawLine, може да се променя стойността на t, като колкото по-малка е толкова по гладка е кривата
void DrawBezier(int vector, Point POld, Point End, int Size)
{
	
	for (double t = 0.0; t <= 1.0; t += 0.03)
	{
		
		Point PP = Bezier(Size - 1, 0, t, vector);
		drawLine(POld, PP);
		POld = PP;
		
	}
	drawLine(End, POld);
}


//Функция за въвеждане на точките с левият бутон на мишката
void mouse(int btn, int state, int x, int y)
{
	int i;
	if (btn == GLUT_LEFT_BUTTON && state == GLUT_DOWN)
	{
		abb = new Point;
		abb->setxy((float)x, (float)y);
		abc.insert(abc.begin() + NUMPOINTS, *abb);
		NUMPOINTS++;
	}

	//когато въведените точки станат повече от две се извиква функцията DrawBezier и се изчертава кривата на Безие
	if (NUMPOINTS > 2)
	{
		DrawBezier(1, abc[0], abc[abc.size()-1], abc.size());
	}

	//когато точките от ходогража станат повече от две се извиква функцията DrawBezier и се изчертава кривата на Безие
	if (hodo.size() > 2)
	{
		
		DrawBezier(2, hodo[0], hodo[hodo.size() - 1], hodo.size());
	}
}



//метод за изчертаване на точки и прави в зависимост къде се извиква
void drawPoint()
{

	glColor4f(0.133333f, 0.545098, 0.133333, 1.0f);
	glEnable(GL_POINT_SMOOTH);
	glEnable(GL_BLEND);
	glPointSize(13);

	glBegin(GL_POINTS);
	for (int i = 0; i < abc.size(); i++)
	{
			glVertex3f(abc[i].x, abc[i].y, 0.4);
	}
	glEnd();

	glColor4fv(color);
	
	glEnable(GL_BLEND);
	glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
	glEnable(GL_LINE_SMOOTH);
	glHint(GL_LINE_SMOOTH_HINT, GL_NICEST);
	glBegin(GL_LINE_STRIP);
	glLineWidth(6);

	glBegin(GL_LINE_STRIP);
	for (int i = 0; i < abc.size(); i++)
	{
		if (abc[i].x > 0 || abc[i].y > 0)
		{
			glVertex3f(abc[i].x, abc[i].y, 0.3);
		}
	}
	glEnd();

}


//Метод за изчертаване на ходографа
void DrawHodograph()
{
	
		hodograph[0].x = (float)(width - (width / 4));
		hodograph[0].y = (float)(height - (height / 2));
		glColor4f(0.133333f, 0.545098, 0.133333, 1.0f);

		glEnable(GL_POINT_SMOOTH);
		glEnable(GL_BLEND);
		glPointSize(13);

		glBegin(GL_POINTS);
		glVertex3f(hodograph[0].x, hodograph[0].y, 0.5);
		glEnd();

		

		glColor4f(0.419608, 0.556863, 0.137255, 1.0f);
		glLineWidth(4);
		hodoy = new Point;
		//Когато броят на контролните точки стане по-голям от единица започва изчертаването на ходографа
		if (abc.size() > 1)
		{


			for (int i = 1; i < abc.size(); i++)
			{
				
					float point1 = (float)((hodograph[0].x - abc[i - 1].x) + abc[i].x);
					float point2 = (float)((hodograph[0].y - abc[i - 1].y) + abc[i].y);
					glColor4f(0.0980392, 0.0980392 ,0.439216, 1.0f);
					glBegin(GL_LINES);
					glVertex3f(hodograph[0].x, hodograph[0].y, 0.3);
					glVertex3f(point1, point2, 0.3);
					glEnd();

					glColor4f(0.419608, 0.556863, 0.137255, 1.0f);
					glPointSize(8);
					glBegin(GL_POINTS);
					glVertex3f(point1, point2, 0.3);
					glEnd();
				}
		}

		//точките на ходографа се вписват в масив
			if (abc.size() > 1)
			{
				if (RMBPressed == false){
					for (int i = abc.size() - 1; i < abc.size(); i++){
						float point1 = (float)((hodograph[0].x - abc[i - 1].x) + abc[i].x);
						float point2 = (float)((hodograph[0].y - abc[i - 1].y) + abc[i].y);
						hodoy->setxy((float)point1, (float)point2);
						hodo.insert(hodo.begin() + NUMPOINTS2, *hodoy);
					}
					NUMPOINTS2++;
				}
			}


			if (hodo.size()>1)
			{
					glColor4f(0.2f, 0.8f, 0.0f, 1.0f);
					glLineWidth(1);

					for (int i = 1; i < hodo.size(); i++)
					{
						glBegin(GL_LINES);
						glVertex3f(hodo[i - 1].x, hodo[i - 1].y, 0.3);
						glVertex3f(hodo[i].x, hodo[i].y, 0.3);
						glEnd();
					}
			}

			

}




//Метод за изчистване на дисплея
void clearall(){
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT); //изчиства дисплея
	//изчиства всички досегашни стойности 
	glLoadIdentity();
	glFlush();
	abc.clear();
	hodo.clear();
	NUMPOINTS = 0;
	NUMPOINTS2 = 0;
}


void display(void)
{
	glClearColor(1, 1, 1, 1.0);
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
	glLoadIdentity();
	drawPoint();
	DrawHodograph();

	glFlush();
}

void reshape(int w, int h)
{
	glViewport(0, 0, width, height);
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	glEnable(GL_DEPTH_TEST);
	glDepthFunc(GL_LEQUAL);
	glOrtho(0.0, width, height, 0, -1.0, 1.0);
	glMatrixMode(GL_MODELVIEW);
	glLoadIdentity();
	glutPostRedisplay();
}





void myGlui()
{
	GLUI *glui = GLUI_Master.create_glui_subwindow(main_window,
		GLUI_SUBWINDOW_TOP);
	glui->add_button("CLEAR", 2, (GLUI_Update_CB)clearall);
	glui->add_column(true);
	glui->add_button("QUIT", 3, (GLUI_Update_CB)exit);
	glui->set_main_gfx_window(main_window);
}



int main(int argc, char **argv)
{
	FreeConsole();
	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_SINGLE | GLUT_RGB | GLUT_DEPTH);
	glutInitWindowSize(width, height);
	glutInitWindowPosition(0, 0);
	main_window = glutCreateWindow("Hodograph");
	glutDisplayFunc(display);
	glutReshapeFunc(reshape);
	glutMouseFunc(mouse);


	myGlui();
	
	
	glutMainLoop();
}