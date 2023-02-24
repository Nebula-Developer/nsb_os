#include <stdio.h>
#include <termios.h>
#include <termcap.h>
#include <stdlib.h>
#include <term.h>

void TestLink() {
    printf("[NSB.OS.Library] Test link from NSB.OS.Library.c\n");
}

void InitTerm() {
    struct termios term;
    tcgetattr(0, &term);
    term.c_lflag &= ~ICANON;
    term.c_lflag &= ~ECHO;
    tcsetattr(0, TCSANOW, &term);
}

void InitTermcap() {
    char *term = getenv("TERM");
    tgetent(NULL, term);
}

void InitLinkLibrary() {
    InitTerm();
    InitTermcap();
}

// Move cursor via termcap
void MoveCursor(int x, int y) {
    char *move = tgetstr("cm", NULL);
    char *buffer = malloc(1024);
    tputs(tgoto(move, x, y - 1), 1, putchar);
}
