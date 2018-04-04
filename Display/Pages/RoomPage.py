from selenium.webdriver.common.by import By
from Locators import Locator


class Room(object):

    def __init__(self, driver):
        self.driver = driver

        self.KindOfRoom = driver.find_element(By.XPATH, Locator.KindOfRoom)




    def click_OpenList(self):
        self.KindOfRoom.click()

    
