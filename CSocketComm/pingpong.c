#include <stdlib.h>
#include <stdio.h>
#include "scomm.h"
#include "cmd.h"

void bm(){
  long k;
  for(int i =0;i<100000;i++){
    k = k+i;
    printf(".");
  }
  printf("\n");
}


int main(int argc, char **argv){
  if(argc<=1){
    fprintf(stderr,"\nUsage: %s [socket]\n\n", argv[0]);
    exit(EXIT_FAILURE);
  }
  // int s = serveSingleClient(argv[1]);
  int s = connectTo(argv[1]);
  if(s<0){
    printf("Bad socket...");
    exit(EXIT_FAILURE);
  }
  CMD c;
  writeCmd(s, Ready);
  do{
    writeCmd(s, Ready);
    readCmd(s); // expecting go
    bm();
    writeCmd(s, Done); //
  } while ((c = readCmd(s)) == Ready ); 
  // we should have read done at this point
  printf("done");
  closeSocket(s);
}