import os, sys
parentddir = os.path.abspath(os.path.join(os.path.dirname(__file__), os.path.pardir))
sys.path.append(parentddir)


class Movement():
   
    @staticmethod
    def Get_and_Convert():
        a = int(input("x "))
        b = int(input("y "))

        if a==0:
            ilex=0
        else:
            ilex=69*(a)+35
        if b==7:
            iley=0
        else:
            iley=69*(b-6)-35
        #print("ilex",ilex)
        #print("iley",iley)
        return ilex,iley
        