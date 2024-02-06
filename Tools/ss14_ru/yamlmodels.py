class YAMLEntity:
    def __init__(self, id, name = None, description = None, suffix = None, parent_id = None, abstract = None):
        self.id = id
        self.name = name
        self.description = description
        self.suffix = suffix
        self.parent_id = parent_id
        self.abstract = abstract


class YAMLElements:
    def __init__(self, items):
        self.elements = list(map(lambda i: self.create_element(i), items))

    def create_element(self, item):
        if not 'id' in item:
            return None

        if item['type'] == 'entity':
            entity = YAMLEntity(item['id'], item['name'] if 'name' in item else None,
                                item['description'] if 'description' in item else None,
                                item['suffix'] if 'suffix' in item else None,
                                item['parent'] if 'parent' in item else None,
                                item['abstract'] if 'abstract' in item else None
                                )
            return entity
        else:
            return None
