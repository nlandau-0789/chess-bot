Valeur de pièces dynamiques

=> le but de l'algorithme est donc de chercher à maximiser la différence de valeur total de ses pièces par rapport à la valeur totale des pièces de son opposant

Chaque pièce sauf le roi gagne de la valeur si elle est protégée (elle en gagne plus si elle est protègée par une pièce avec peu de valeur)
Plus une pièce est près du roi ennemi, plus elle gagne en valeur
Plus un pion est près de la promo, plus il a de valeur
Le roi à pour valeur -5 + somme des pièces près de lui (pondéré par la distance (-30% par cas plus loin))

