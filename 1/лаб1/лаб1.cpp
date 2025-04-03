// лаб1.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include <iostream>
#include <cmath> 
#include <tuple>
#include <string>

using namespace std;

float f(float x) {
    return 2 * x - 4 * cos(x) - 0.6;
}

float gSIM(float x) {
    return x - 0.1 * (2*x - 4*cos(x) - 0.6);
}

float gNM(float x) {
    return x - (2 * x - 4 * cos(x) - 0.6) / (2 + 4 * sin(x)); 
}

float gMNM(float x) {
    return x - (2 * x - 4 * cos(x) - 0.6) / (5.98998);
}

//метод простых итреаций 
tuple<float, int> SIM(float x0, float eps, float sig) {
    float new_x, x{ x0 };
    int n = 0;
    
    cout << "n\txn\txn+1\t|xn+1 = xn|\t|f(xn+1)|" << endl;

    while (true) {
        new_x = gSIM(x);
        n++;

        cout << n << "\t" << x << "\t" << new_x << "\t" << abs(new_x - x) << "\t" << abs(f(new_x)) << endl;

        if (abs(new_x - x) <= eps and abs(f(new_x)) <= sig) {
            return std::make_tuple(new_x, n);
        }

        x = new_x;
    }
}

tuple<float, int> NM(float x0, float eps, float sig) {
    float new_x, x{ x0 };
    int n = 0;

    cout << "n\txn\txn+1\t|xn+1 = xn|\t|f(xn+1)|" << endl;

    while (true) {
        new_x = gNM(x);
        n++;

        cout << n << "\t" << x << "\t" << new_x << "\t" << abs(new_x - x) << "\t" << abs(f(new_x)) << endl;

        if (abs(new_x - x) <= eps and abs(f(new_x)) <= sig) {
            return std::make_tuple(new_x, n);
        }

        x = new_x;
    }
}

tuple<float, int> MNM(float x0, float eps, float sig) {
    float new_x, x{ x0 };
    int n = 0;

    cout << "n\txn\txn+1\t|xn+1  xn|\t|f(xn+1)|" << endl;

    while (true) {
        new_x = gMNM(x);
        n++;

        cout << n << "\t" << x << "\t" << new_x << "\t" << abs(new_x - x) << "\t" << abs(f(new_x)) << endl;

        if (abs(new_x - x) <= eps and abs(f(new_x)) <= sig) {
            return std::make_tuple(new_x, n);
        }

        x = new_x;
    }
}


int main()
{
    setlocale(LC_ALL, "Rus");

    float eps = 0.001;
    float sig = 0.01; 
    float x0 = 1.5; 
    float a = 1; 
    float b = 1.5; 

    tuple<float, int> resSIM = SIM(x0, eps, sig); 
    cout << "x = " + to_string(get<0>(resSIM)) + "\n n = " + to_string(get<1>(resSIM)) << endl;

    tuple<float, int> resNM = NM(x0, eps, sig);
    cout << "x = " + to_string(get<0>(resNM)) + "\n n = " + to_string(get<1>(resNM)) << endl;

    tuple<float, int> resMNM = MNM(x0, eps, sig);
    cout << "x = " + to_string(get<0>(resMNM)) + "\n n = " + to_string(get<1>(resMNM)) << endl;
} 

