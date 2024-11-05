using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace testgame
{
    internal class itemgenerator
    {
        

        public 
        int item_id;
        string item_name;
        int item_level;
        int item_rarity;
        int item_type;
        int hp;
        int str;
        int def;
        int Para;
        string efsun1="default", efsun2="default";


        public string create(int level)
        {
            Random random = new Random();
            
                
           
              item_level = random.Next(level, level+1);
            item_rarity = random.Next(1, 11);
            item_type=random.Next(1, 5);
            hp = random.Next(level/2,level*2);
            str = random.Next(level / 2, level);
            def = random.Next(level / 2, level);
            Para=random.Next(level*2,level*4);
            return Convert.ToString(item_id+"*"+/*item_name+*/"Test İtem *"+item_level + "*" + item_rarity + "*" + item_type + "*" + hp + "*" + str + "*" + def + "*"+efsun1+"*"+efsun2+"*"+Para);
        }


    }
}
