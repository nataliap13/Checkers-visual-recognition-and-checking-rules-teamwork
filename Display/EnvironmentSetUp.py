import unittest
import datetime
from selenium import webdriver
class EnvironmentSetup(unittest.TestCase):
    """Inicjalizacja wszystkiego"""

#setUP contains the browser setup attributes
    def setUp(self):
        """Inicjalizacja drivera"""
        
        self.driver = webdriver.Chrome('C:\\Users\\Piotr\\Desktop\\Display\\chromedriver.exe')
        print ("Run Started at :"+str(datetime.datetime.now()))
        print("Chrome Environment Set Up")
        print("------------------------------------------------------------------")
        self.driver.implicitly_wait(20)
        self.driver.maximize_window()

#tearDown method just to close all the browser instances and then quit
    def tearDown(self):
        """Zamkniecie sesji"""
        if (self.driver!=None):
            print("------------------------------------------------------------------")
            print("Test Environment Destroyed")
            print("Run Completed at :" + str(datetime.datetime.now()))
            self.driver.close()
            self.driver.quit()