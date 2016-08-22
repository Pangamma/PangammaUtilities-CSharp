// Extra spaces? No problem.

int? n = " 9 ".ToNullable<int>(); // --> 9

bool? b = "true".ToNullable<bool>(); // --> true  

bool? b = "this will not parse".ToNullable<bool>(); // --> null


string s = null;

int n = s.ToNullable<int>() ?? 2;   // --> null --> 2
