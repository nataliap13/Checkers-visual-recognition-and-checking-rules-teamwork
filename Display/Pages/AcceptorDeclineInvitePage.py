from selenium.webdriver.common.by import By
from Locators import Locator

from selenium.webdriver.support.wait import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from selenium.webdriver.common.by import By

class AcceptorDeclineInvite(object):

    def __init__(self, driver):
        self.driver = driver

        self.AcceptInvite = driver.find_element(By.XPATH, Locator.AcceptInvite)
        self.DeclineInvite = driver.find_element(By.XPATH, Locator.DeclineInvite)
        self.Condition=driver.find_element(By.XPATH, Locator.Condition)



    def click_AcceptInvite(self,driver):
        self.AcceptInvite.click()
        el=WebDriverWait(driver, 300).until(
        EC.visibility_of_element_located((By.XPATH, Locator.TakeSide2)))

    def click_DeclineInvite(self):
        self.DeclineInvite.click()
    
    def check_Condition(self):
        if(self.Condition.is_displayed()):
            return True
        return False
        


    
