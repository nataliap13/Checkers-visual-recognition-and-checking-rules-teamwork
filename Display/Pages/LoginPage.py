from selenium.webdriver.common.by import By
from Locators import Locator


class Login(object):

    def __init__(self, driver):
        self.driver = driver
        
        self.Login = driver.find_element(By.XPATH, Locator.Login)
        self.Password = driver.find_element(By.XPATH, Locator.Password)
        self.EnterButton=driver.find_element(By.XPATH, Locator.EnterButton)


    def setLogin(self, login):
        self.Login.clear()
        self.Login.send_keys(login)

    def setPassword(self, password):
        self.Password.clear()
        self.Password.send_keys(password)
    
    def click_EnterButton(self):
        self.EnterButton.click()
    
