#include <iostream>
#include <iomanip>
#include <cmath> 
#include <tuple>
#include <string>

using namespace std;

//Рабочие формулы метода итераций
static tuple <float, float, float> gIM(float x, float y, float z) {
    tuple<float, float, float> res = {
        (1.0 + (1.0/3.0)*y - (1.0/3.0)*z),
        (1.0 - (2.0/5.0)*x + (2.0/5.0)*z),
        ((1.0/3.0) + (1.0/3.0)*x + (1.0/3.0)*y)
    };

    return res;
}

//Рабочие формулы метода зейделя
static tuple <float, float, float> gZM(float x, float y, float z) {
    float x1{ float((1.0 + (1.0 / 3.0) * y - (1.0 / 3.0) * z)) };
    float y1{ float((1.0 - (2.0 / 5.0) * x1 + (2.0 / 5.0) * z)) };
    float z1{ float(((1.0 / 3.0) + (1.0 / 3.0) * x1 + (1.0 / 3.0) * y1)) };
   
    tuple<float, float, float> res = {x1, y1, z1};

    return res;
}

//метод простых итреаций 
static tuple<float, float, float> IM(float x0, float y0, float z0, float eps) {
    tuple<float, float, float> xyz = { x0, y0, z0 };
    tuple<float, float, float> new_xyz = { 0, 0, 0 };
    int n = 0;

    cout << "n\t|    xn   |   xn+1  ||xn+1-xn||    yn   |   yn+1  ||yn+1-yn||    zn   |   zn+1  ||zn+1-zn|" << endl;
    cout << "________|_________|_________|_________|_________|_________|_________|_________|_________|_________" << endl;

    while (true) {
        new_xyz = gIM(get<0>(xyz), get<1>(xyz), get<2>(xyz));

        cout << n;
        cout << "\t| "; printf("%6f", get<0>(xyz));
        cout << "| "; printf("%6f", get<0>(new_xyz));
        cout << "| "; printf("%6f", abs(get<0>(new_xyz) - get<0>(xyz)));
        cout << "| "; printf("%6f", get<1>(xyz));
        cout << "| "; printf("%6f", get<1>(new_xyz));
        cout << "| "; printf("%6f", abs(get<1>(new_xyz) - get<1>(xyz)));
        cout << "| "; printf("%6f", get<2>(xyz));
        cout << "| "; printf("%6f", get<2>(new_xyz));
        cout << "| "; printf("%6f", abs(get<2>(new_xyz) - get<2>(xyz)));
        cout << endl;

        if (abs(get<0>(new_xyz) - get<0>(xyz)) <= eps and abs(get<1>(new_xyz) - get<1>(xyz)) <= eps and abs(get<2>(new_xyz) - get<2>(xyz)) <= eps){
            return new_xyz;
        }

        xyz = new_xyz;
        n++;
    }
}


//метод Зейделя
static tuple<float, float, float> ZM(float x0, float y0, float z0, float eps) {
    tuple<float, float, float> xyz = { x0, y0, z0 };
    tuple<float, float, float> new_xyz = { 0, 0, 0 };
    int n = 0;

    cout << "n\t|    xn   |   xn+1  ||xn+1-xn||    yn   |   yn+1  ||yn+1-yn||    zn   |   zn+1  ||zn+1-zn|" << endl;
    cout << "________|_________|_________|_________|_________|_________|_________|_________|_________|_________" << endl;

    while (true) {
        new_xyz = gZM(get<0>(xyz), get<1>(xyz), get<2>(xyz));
        
        cout << n;
        cout << "\t| "; printf("%6f", get<0>(xyz));
        cout << "| "; printf("%6f", get<0>(new_xyz));
        cout << "| "; printf("%6f", abs(get<0>(new_xyz) - get<0>(xyz)));
        cout << "| "; printf("%6f", get<1>(xyz));
        cout << "| "; printf("%6f", get<1>(new_xyz));
        cout << "| "; printf("%6f", abs(get<1>(new_xyz) - get<1>(xyz)));
        cout << "| "; printf("%6f", get<2>(xyz));
        cout << "| "; printf("%6f", get<2>(new_xyz));
        cout << "| "; printf("%6f", abs(get<2>(new_xyz) - get<2>(xyz)));
        cout << endl;

        if (abs(get<0>(new_xyz) - get<0>(xyz)) <= eps and abs(get<1>(new_xyz) - get<1>(xyz)) <= eps and abs(get<2>(new_xyz) - get<2>(xyz)) <= eps) {
            return new_xyz;
        }

        xyz = new_xyz;
        n++;
    }
}

int main()
{
    setlocale(LC_ALL, "Rus");

    float eps = 0.001;
    float x0 = 1.0;
    float y0 = 1.0;
    float z0 = 1.0 / 3.0;

    tuple<float, float, float> resIM = IM(x0, y0, z0, eps);
    cout << "x = " + to_string(get<0>(resIM)) + "\n y = " + to_string(get<1>(resIM)) + "\n z = " + to_string(get<2>(resIM)) << endl;

    tuple<float, float, float> resZM = ZM(x0, y0, z0, eps);
    cout << "x = " + to_string(get<0>(resZM)) + "\n y = " + to_string(get<1>(resZM)) + "\n z = " + to_string(get<2>(resZM)) << endl;
}