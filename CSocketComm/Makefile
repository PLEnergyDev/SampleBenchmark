
pingpong: pingpong.c scomm.o cmd.o mmul.o
	gcc -o pingpong pingpong.c scomm.o cmd.o mmul.o

mmul.o: mmul.c mmul.h
	gcc -c mmul.c -o mmul.o

cmd.o: cmd.c cmd.h
	gcc -c cmd.c -o cmd.o

scomm.o: scomm.c scomm.h
	gcc -c scomm.c -o scomm.o

