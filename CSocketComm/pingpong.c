#include <stdlib.h>
#include <stdio.h>
#include "scomm.h"
#include "cmd.h"

void bm(){
  long k;
  printf("running...\n");
  for(int i =0;i<100000;i++){
    k = k+i;
  }
  printf("done\n");
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
    c=readCmd(s); // expecting go
    printf("received (expected go): %s", tos(c));
    bm();
    writeCmd(s, Done); //
  } while ((c = readCmd(s)) == Ready ); 
  // we should have read done at this point
  printf("\ndone\n");
  closeSocket(s);
}
