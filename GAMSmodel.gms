
$set startcity i1

sets
         i / i1 * i6 /;

alias (i, j);

Parameter d(i,j) 'distances in kilometers';
$include "C:\Users\User\Desktop\SCHOOL\Unity Model\fire sandbox\Assets\Scripts\GAMSinput.txt";
display d;

d(i, j) = max(d(i, j),d(j, i));

scalar m 'num of salesman' /1/;
scalar n 'nodes';
n = card(i);
scalar p 'cities per salesman';
p = n;

set arcs(i, j);
arcs(i, j)$d(i, j)= yes;

binary variables x(i, j);
positive variables u(i);
variable z;

* define the start city and the cities that aren't the start city
set i0(i) /%startcity%/;
set i2(i);
i2(i)$(not i0(i)) = yes;

equations
         start
         end
         assign1(i)
         assign2(j)
         sec(i, j)   'subtour elim'
         cost
;

start.. sum(i2(j), x('%startcity%', j)) =e= m;
end.. sum(i2(i), x(i, '%startcity%')) =e= m;
assign1(i2(i)).. sum(arcs(i, j), x(i, j)) =e= 1;
assign2(i2(j)).. sum(arcs(i, j), x(i, j)) =e= 1;
sec(arcs(i, j))$(i2(i) and i2(j)).. u(i) - u(j) + p*x(i, j) =L= p-1;
cost.. z =e= sum(arcs, d(arcs)*x(arcs));

option optcr=0;
option iterlim=1000000;

model mtsp /all/;
solve mtsp minimizing z using mip;

display x.l;

