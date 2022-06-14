#include<stdlib.h>
#include<stdio.h>
#include<time.h>
typedef struct {
    int rows, cols;
    double* data; // (rows * cols) doubles, row-major order
} matrix;

static long get_nanos(void) {
    struct timespec ts;
    timespec_get(&ts, TIME_UTC);
    return (long)ts.tv_sec * 1000000000L + ts.tv_nsec;
}

void im(matrix* m, int r, int c) {
    m->data = (double*)malloc(sizeof(double) * r * c);
    m->rows = r;
    m->cols = c;
}

void mmul() {
    int rRows = 640;
    matrix R, A, B;
    int rCols = rRows;
    int aCols = rCols;
    int bCols = aCols;
    im(&R, rRows, rCols);
    im(&A, rRows, rCols);
    im(&B, rRows, rCols);
    int r, c, k;
    for (r = 0; r < rRows; r++) {
        for (c = 0; c < rCols; c++) {
            double sum = 0.0;
            for (k = 0; k < aCols; k++)
                sum += A.data[r * aCols + k] * B.data[k * bCols + c];
            R.data[r * rCols + c] = sum;
        }
    }
}
