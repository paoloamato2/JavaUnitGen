clear all;
clc;

mat1 = table2array(readtable('1thread.csv'));
x1 = mat1(:,1);
y1 = mat1(:,2);

mat2 = table2array(readtable('2thread.csv'));
x2 = mat2(:,1);
y2 = mat2(:,2);

figure(); 
plot(x1,y1);
hold on;
plot(x2,y2);
legend('Minimizzazione con singolo thread','Minimizzazione con due threads');
hold off;
title('Valutazione del tempo di minimizzazione rispetto alla dimensione della test suite');