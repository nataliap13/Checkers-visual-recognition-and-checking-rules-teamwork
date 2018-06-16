from selenium.webdriver.common.by import By
from Locators import Locator


class Welcome(object):

    def __init__(self, driver):
        self.driver = driver

        #self.GuestButton = driver.find_element(By.XPATH, Locator.GuestButton)
        #self.ZalogujButton=driver.find_element(By.XPATH, Locator.ZalogujButton)
        self.ZalogujButton=driver.find_element(By.XPATH, Locator.nowyzaloguj)



    #def click_GuestButton(self):
        #self.GuestButton.click()
    
    def click_ZalogujButton(self):
        self.ZalogujButton.click()
