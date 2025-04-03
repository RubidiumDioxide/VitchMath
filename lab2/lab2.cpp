#include <iostream>
#include <iomanip>
#include <cmath> 
#include <tuple>
#include <string>

using namespace std;

//Рабочие формулы метода итераций
static tuple <float, float> gIM(float x, float y) {
    tuple<float, float> res = { 
        (x - ((pow(x, 2) + pow(y, 2) - 17) / 20) + (3 * ((-3) * x + y - 1) / 10)),
        (y - (3 * (pow(x, 2) + pow(y, 2) - 17) / 20) - (((-3) * x + y - 1) / 10)) 
    };

    return res;
}

//Рабочие формулы метода Ньютона
static tuple <float, float> gNM(float x, float y) {
    tuple<float, float> res = { 
        (pow(x, 2) + pow(y, 2) -2 * y + 17)/(2 * x + 6 * y),
        ((3 * pow(x, 2) + 3 * pow(y, 2) + 2 * x + 51)/(2 * x + 6 * y))
    };

    return res;
}

//метод простых итреаций 
static tuple<float, float> IM(float x0, float y0, float eps) {
    tuple<float, float> xy = { x0, y0 };
    tuple<float, float> new_xy = { 0, 0 };
    int n = 1;

    cout << "n\txn\t\txn+1\t\t|xn+1 - xn|\tyn\t\tyn+1\t\t|yn+1 - yn|" << endl;

    while (true) {
        new_xy = gIM(get<0>(xy), get<1>(xy));

        cout << n; 
        cout << "\t"; printf("%5f", get<0>(xy));
        cout << "\t"; printf("%5f", get<0>(new_xy));
        cout << "\t"; printf("%5f", abs(get<0>(new_xy) - get<0>(xy)));
        cout << "\t"; printf("%5f", get<1>(xy));
        cout << "\t"; printf("%5f", get<1>(new_xy));
        cout << "\t"; printf("%5f", abs(get<1>(new_xy) - get<1>(xy)));
        cout  << endl;

        if (abs(get<0>(new_xy) - get<0>(xy)) <= eps and abs(get<1>(new_xy) - get<1>(xy)) <= eps) {
            return new_xy;
        }

        xy = new_xy; 
        n++; 
    }
}

//Метод Ньютона 
static tuple<float, float> NM(float x0, float y0, float eps) {
    tuple<float, float> xy = { x0, y0 };
    tuple<float, float> new_xy = { 0, 0 };
    int n = 1;

    cout << "n\txn\t\txn+1\t\t|xn+1 - xn|\tyn\t\tyn+1\t\t|yn+1 - yn|" << endl;

    while (true) {
        new_xy = gNM(get<0>(xy), get<1>(xy));

        cout << n;
        cout << "\t"; printf("%5f", get<0>(xy));
        cout << "\t"; printf("%5f", get<0>(new_xy));
        cout << "\t"; printf("%5f", abs(get<0>(new_xy) - get<0>(xy)));
        cout << "\t"; printf("%5f", get<1>(xy));
        cout << "\t"; printf("%5f", get<1>(new_xy));
        cout << "\t"; printf("%5f", abs(get<1>(new_xy) - get<1>(xy)));
        cout << endl;

        if (abs(get<0>(new_xy) - get<0>(xy)) <= eps and abs(get<1>(new_xy) - get<1>(xy)) <= eps) {
            return new_xy;
        }

        xy = new_xy;
        n++; 
    }
}
int main(){
    setlocale(LC_ALL, "Rus");

    float eps = 0.001;
    float x0 = 1.0;
    float y0 = 3.0;

    tuple<float, float> resSIM = IM(x0, y0, eps);
    cout << "x = " + to_string(get<0>(resSIM)) + "\n y = " + to_string(get<1>(resSIM)) << endl;

    tuple<float, float> resNM = NM(x0, y0, eps);
    cout << "x = " + to_string(get<0>(resNM)) + "\n y = " + to_string(get<1>(resNM)) << endl;
} 