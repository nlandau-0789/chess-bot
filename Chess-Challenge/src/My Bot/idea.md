Valeur de pièces dynamiques

=> le but de l'algorithme est donc de chercher à maximiser la différence de valeur total de ses pièces par rapport à la valeur totale des pièces de son opposant

Chaque pièce sauf le roi gagne de la valeur si elle est protégée (elle en gagne plus si elle est protègée par une pièce avec peu de valeur)
Plus une pièce est près du roi ennemi, plus elle gagne en valeur
Plus un pion est près de la promo, plus il a de valeur
Le roi gagne de la valeur si des pièces sont près de lui (pondéré par la distance)
Notre roi perd de la valeur si il perd du mouvement
Le roi ennemi en gagne dans ce cas 

+il faut trouver un moyen de le faire faire des échecs et mat