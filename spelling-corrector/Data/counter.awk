#!/usr/bin/awk -f
#remove punctuations, count distinct words
{
gsub(/[[:punct:]]/, "")
for (i = 1; i <= NF; i++)
    if ($i ~ /^[[:alnum:]]/)
	wc[tolower($i)]++
};
END {for (w in wc)
        print w, wc[w]
}
