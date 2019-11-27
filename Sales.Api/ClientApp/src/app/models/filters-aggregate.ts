import { NamedItem } from '@/models/named-item';

export interface FiltersAggregate {
  countries: NamedItem[];
  itemTypes: NamedItem[];
  orderPriorities: NamedItem[];
  regions: NamedItem[];
  regionsCountries: NamedItem[];
  salesChannels: NamedItem[];
}
