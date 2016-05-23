#!/bin/bash
#$ -cwd
#$ -j y
#$ -S /bin/bash
echo 100000 10 | /opt/mpich2/gnu/bin/mpiexec -n 12 -f /home/B12422/maq ./tp2.out